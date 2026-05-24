using AForge;
using BioImager;
using BioLib;
using BruTile;
using OpenSlideGTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BioImager
{
    public class SlideRenderer
    {
        private readonly SlideGLArea _glArea;
        private OpenSlideBase _openSlideBase;
        private SlideBase _slideBase;
        private bool _useOpenSlide;
        private BioLib.TileCache _tileCache;
        private OpenSlideGTK.TileCache _openTileCache;
        private HashSet<TileIndex> _uploadedTiles = new();
        private int _currentLevel = -1;
        private bool _hasPendingView;
        private PointD _pendingPyramidalOrigin;
        private int _pendingViewportWidth;
        private int _pendingViewportHeight;
        private double _pendingResolution;
        private ZCT _pendingCoordinate;
        private readonly SemaphoreSlim _renderSemaphore = new(1, 1);
        private readonly SemaphoreSlim _fetchSemaphore = new(6, 6);
        private static readonly TimeSpan RemoteTileFetchTimeout = TimeSpan.FromSeconds(60);
        private readonly HashSet<TileIndex> _pendingTileFetches = new();
        private readonly object _pendingTileFetchLock = new();
        private int _redrawQueued = 0;
        public string DebugSummary { get; private set; } = string.Empty;

        public SlideRenderer(SlideGLArea glArea)
        {
            _glArea = glArea;
            _glArea.GLReady += OnGlReady;
        }

        public void SetSource(OpenSlideBase source)
        {
            _openSlideBase = source;
            _slideBase = null;
            _useOpenSlide = true;
            _openTileCache = new OpenSlideGTK.TileCache(source, 200);
            ClearCache();
        }

        public void SetSource(SlideBase source)
        {
            _slideBase = source;
            _openSlideBase = null;
            _useOpenSlide = false;
            _tileCache = new BioLib.TileCache(source, 200);
            ClearCache();
        }

        public void ClearCache()
        {
            _glArea.ClearTextureCache();
            _uploadedTiles.Clear();
            _currentLevel = -1;
            lock (_pendingTileFetchLock)
            {
                _pendingTileFetches.Clear();
            }
        }

        public async Task UpdateViewAsync(
            PointD pyramidalOrigin,
            int viewportWidth,
            int viewportHeight,
            double resolution,
            ZCT coordinate)
        {
            _pendingPyramidalOrigin = pyramidalOrigin;
            _pendingViewportWidth = viewportWidth;
            _pendingViewportHeight = viewportHeight;
            _pendingResolution = resolution;
            _pendingCoordinate = coordinate;
            _hasPendingView = true;

            if (_openSlideBase == null && _slideBase == null)
                return;
            if (viewportWidth <= 1 && viewportHeight <= 1)
                return;
            if (!_glArea.IsGLReady)
                return;
            if (!_renderSemaphore.Wait(0))
            {
                return;
            }

            try
            {
                var schema = _useOpenSlide ? _openSlideBase.Schema : _slideBase.Schema;
                int level = TileUtil.GetLevel(schema.Resolutions, resolution);
                var levelRes = schema.Resolutions[level];
                double levelUnitsPerPixel = levelRes.UnitsPerPixel;

                if (level != _currentLevel)
                {
                    _glArea.ReleaseLevelTextures(_currentLevel);
                    _currentLevel = level;
                }

                double minX = pyramidalOrigin.X;
                double width = viewportWidth * resolution;
                double height = viewportHeight * resolution;
                var viewportExtent = new Extent(minX, -pyramidalOrigin.Y - height, minX + width, -pyramidalOrigin.Y);

                double tileMarginX = levelRes.TileWidth * levelUnitsPerPixel;
                double tileMarginY = levelRes.TileHeight * levelUnitsPerPixel;
                var fetchExtent = new Extent(
                    viewportExtent.MinX - tileMarginX,
                    viewportExtent.MinY - tileMarginY,
                    viewportExtent.MaxX + tileMarginX,
                    viewportExtent.MaxY + tileMarginY);

                var lookupExtent = BioLib.ExtentEx.WorldToPixelInvertedY(fetchExtent, levelUnitsPerPixel);
                var tileInfos = schema.GetTileInfos(lookupExtent, level).ToList();
                var fallbackInfos = schema.GetTileInfos(fetchExtent, level).ToList();
                if (fallbackInfos.Count > 0)
                {
                    var seen = new HashSet<TileIndex>(tileInfos.Select(t => t.Index));
                    foreach (var info in fallbackInfos)
                    {
                        if (seen.Add(info.Index))
                            tileInfos.Add(info);
                    }
                }

                DebugSummary = $"WSI level={level} res={resolution:F2} tiles={tileInfos.Count}";
                Console.WriteLine($"[SlideRenderer] level={level} res={resolution:F4} viewport={viewportWidth}x{viewportHeight} origin=({pyramidalOrigin.X:F1},{pyramidalOrigin.Y:F1}) tiles={tileInfos.Count}");

                var renderInfos = new List<TileRenderInfo>();
                var pendingFetches = new List<(TileInfo Tile, Task<byte[]> FetchTask)>();
                foreach (var tileInfo in tileInfos)
                {
                    if (_glArea.HasTileTexture(tileInfo.Index))
                    {
                        renderInfos.Add(CalculateScreenPosition(
                            tileInfo,
                            pyramidalOrigin,
                            resolution,
                            level,
                            schema));
                        continue;
                    }

                    bool alreadyPending;
                    lock (_pendingTileFetchLock)
                    {
                        alreadyPending = !_pendingTileFetches.Add(tileInfo.Index);
                    }

                    if (!alreadyPending)
                    {
                        pendingFetches.Add((tileInfo, FetchTileAsync(tileInfo, level, coordinate, RemoteTileFetchTimeout)));
                        renderInfos.Add(CalculateScreenPosition(
                            tileInfo,
                            pyramidalOrigin,
                            resolution,
                            level,
                            schema));
                    }
                }

                foreach (var pending in pendingFetches)
                {
                    _ = ProcessPendingFetchAsync(
                        pending.Tile,
                        pending.FetchTask,
                        pyramidalOrigin,
                        resolution,
                        level,
                        schema);
                }

                DebugSummary = $"{DebugSummary} cached={_glArea.CachedTextureCount} renderInfos={renderInfos.Count}";
                Console.WriteLine($"[SlideRenderer] textures={_glArea.CachedTextureCount} renderInfos={renderInfos.Count}");
                _glArea.SetTilesToRender(renderInfos);
                _glArea.RequestRedraw();
            }
            finally
            {
                _renderSemaphore.Release();
            }
        }

        private void OnGlReady(object sender, EventArgs e)
        {
            if (!_hasPendingView)
                return;

            _ = _glArea.BeginInvoke(new System.Action(async () =>
            {
                try
                {
                    await UpdateViewAsync(
                        _pendingPyramidalOrigin,
                        _pendingViewportWidth,
                        _pendingViewportHeight,
                        _pendingResolution,
                        _pendingCoordinate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[SlideRenderer] GLReady refresh failed: {ex.Message}");
                }
            }));
        }

        private async Task<byte[]> FetchTileAsync(TileInfo tileInfo, int level, ZCT coordinate, TimeSpan timeout)
        {
            try
            {
                using var cts = new CancellationTokenSource(timeout);
                await _fetchSemaphore.WaitAsync(cts.Token);
                try
                {
                    if (_useOpenSlide)
                    {
                        return await _openTileCache.GetTile(new OpenSlideGTK.Info(coordinate, tileInfo.Index, tileInfo.Extent, level));
                    }
                    else
                    {
                        return await _tileCache.GetTile(new BioLib.TileInformation(tileInfo.Index, tileInfo.Extent, coordinate));
                    }
                }
                finally
                {
                    _fetchSemaphore.Release();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tile {tileInfo.Index}: {ex.Message}");
                return null;
            }
        }

        private async Task ProcessPendingFetchAsync(
            TileInfo tile,
            Task<byte[]> fetchTask,
            PointD pyramidalOrigin,
            double resolution,
            int level,
            ITileSchema schema)
        {
            try
            {
                byte[] tileData = await fetchTask.ConfigureAwait(false);
                if (tileData == null)
                {
                    Console.WriteLine($"[SlideRenderer] fetch returned null for {tile.Index}");
                    return;
                }

                int tW = schema.Resolutions[level].TileWidth;
                int tH = schema.Resolutions[level].TileHeight;

                await RunOnUiThreadAsync(() =>
                {
                    if (_glArea.UploadTileTexture(tile.Index, tileData, tW, tH))
                    {
                        _uploadedTiles.Add(tile.Index);
                        _glArea.RequestRedraw();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SlideRenderer] pending fetch failed tile={tile.Index} err={ex.Message}");
            }
            finally
            {
                lock (_pendingTileFetchLock)
                {
                    _pendingTileFetches.Remove(tile.Index);
                }

                if (Interlocked.Exchange(ref _redrawQueued, 1) == 0)
                {
                    try
                    {
                        await RunOnUiThreadAsync(() => _glArea.RequestRedraw());
                    }
                    finally
                    {
                        Interlocked.Exchange(ref _redrawQueued, 0);
                    }
                }
            }
        }

        private Task RunOnUiThreadAsync(System.Action action)
        {
            if (_glArea.IsDisposed)
                return Task.CompletedTask;

            if (_glArea.InvokeRequired)
            {
                var tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                try
                {
                    _glArea.BeginInvoke(new System.Action(() =>
                    {
                        try
                        {
                            action();
                            tcs.TrySetResult(true);
                        }
                        catch (Exception ex)
                        {
                            tcs.TrySetException(ex);
                        }
                    }));
                }
                catch (Exception ex)
                {
                    tcs.TrySetException(ex);
                }

                return tcs.Task;
            }

            action();
            return Task.CompletedTask;
        }

        private TileRenderInfo CalculateScreenPosition(
            TileInfo tile,
            PointD pyramidalOrigin,
            double viewResolution,
            int level,
            ITileSchema schema)
        {
            var tileExtent = tile.Extent;
            var clippedExtent = new Extent(
                Math.Max(tileExtent.MinX, schema.Extent.MinX),
                Math.Max(tileExtent.MinY, schema.Extent.MinY),
                Math.Min(tileExtent.MaxX, schema.Extent.MaxX),
                Math.Min(tileExtent.MaxY, schema.Extent.MaxY));

            if (clippedExtent.MaxX <= clippedExtent.MinX || clippedExtent.MaxY <= clippedExtent.MinY)
                return new TileRenderInfo(tile.Index, -9999, -9999, 0, 0);

            double originScreenX = pyramidalOrigin.X / viewResolution;
            double originScreenY = pyramidalOrigin.Y / viewResolution;

            float screenX = (float)(clippedExtent.MinX / viewResolution - originScreenX);
            float screenY = (float)(-clippedExtent.MaxY / viewResolution - originScreenY);
            float screenWidth = (float)((clippedExtent.MaxX - clippedExtent.MinX) / viewResolution);
            float screenHeight = (float)((clippedExtent.MaxY - clippedExtent.MinY) / viewResolution);

            return new TileRenderInfo(tile.Index, screenX, screenY, screenWidth, screenHeight);
        }

        public int CurrentLevel => _currentLevel;
        public int CachedTextureCount => _uploadedTiles.Count;
    }
}
