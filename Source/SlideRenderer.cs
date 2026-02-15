using AForge;
using BioImager;
using BioLib;
using BruTile;
using OpenSlideGTK;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public SlideRenderer(SlideGLArea glArea)
        {
            _glArea = glArea;
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
        }

        public async Task UpdateViewAsync(
            PointD pyramidalOrigin,
            int viewportWidth,
            int viewportHeight,
            double resolution,
            ZCT coordinate)
        {
            if (_openSlideBase == null && _slideBase == null)
                return;
            if (viewportWidth <= 1 && viewportHeight <= 1)
                return;
            var schema = _useOpenSlide ? _openSlideBase.Schema : _slideBase.Schema;

            int level = TileUtil.GetLevel(schema.Resolutions, resolution);
            var levelRes = schema.Resolutions[level];
            double levelUnitsPerPixel = levelRes.UnitsPerPixel;

            if (level != _currentLevel)
            {
                _glArea.ReleaseLevelTextures(_currentLevel);
                _currentLevel = level;
            }

            // Calculate world extent for the viewport
            double minX = pyramidalOrigin.X * resolution;
            double minY = -pyramidalOrigin.Y * resolution;
            double width = viewportWidth * resolution;
            double height = viewportHeight * resolution;
            var worldExtent = new Extent(minX, minY - height, minX + width, minY);

            // Get tiles that intersect the viewport
            var tileInfos = schema.GetTileInfos(worldExtent, level).ToList();

            if (tileInfos.Count == 0)
            {
                var pixelExtent = OpenSlideGTK.ExtentEx.WorldToPixelInvertedY(worldExtent, resolution);
                tileInfos = schema.GetTileInfos(pixelExtent, level).ToList();
            }

            if (_openSlideBase != null)
                await _openSlideBase.FetchTilesAsync(tileInfos.ToList(), level, coordinate);
            else
                await _slideBase.FetchTilesAsync(tileInfos.ToList(), level, coordinate, pyramidalOrigin, new AForge.Size(viewportWidth, viewportHeight));

            var renderInfos = new List<TileRenderInfo>();
            foreach (var tileInfo in tileInfos)
            {
                if (!_glArea.HasTileTexture(tileInfo.Index))
                {
                    byte[] tileData;
                    if (_useOpenSlide)
                        tileData = await _openSlideBase.GetTileAsync(tileInfo);
                    else
                        tileData = await _slideBase.GetTileAsync(tileInfo, coordinate);
                    if (tileData != null)
                    {
                        // Fix: Calculate actual tile dimensions from extent, do not hardcode 256.
                        // This handles edge tiles that might be smaller.
                        int tW = (int)Math.Round(tileInfo.Extent.Width / levelUnitsPerPixel);
                        int tH = (int)Math.Round(tileInfo.Extent.Height / levelUnitsPerPixel);

                        _glArea.UploadTileTexture(tileInfo.Index, tileData, tW, tH);
                        _uploadedTiles.Add(tileInfo.Index);
                    }
                }

                if (_glArea.HasTileTexture(tileInfo.Index))
                {
                    var renderInfo = CalculateScreenPosition(
                        tileInfo,
                        pyramidalOrigin,
                        resolution,
                        level,
                        schema);
                    renderInfos.Add(renderInfo);
                }
            }

            _glArea.SetTilesToRender(renderInfos);
            _glArea.RequestRedraw();
        }

        private async Task<byte[]> FetchTileAsync(TileInfo tileInfo, int level, ZCT coordinate)
        {
            try
            {
                if (_useOpenSlide)
                {
                    var bt = await _openTileCache.GetTile(new OpenSlideGTK.Info(coordinate, tileInfo.Index, tileInfo.Extent, level));
                    return bt;
                }
                else
                {
                    return await _tileCache.GetTile(new BioLib.TileInformation(tileInfo.Index, tileInfo.Extent, coordinate));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tile {tileInfo.Index}: {ex.Message}");
                return null;
            }
        }

        private TileRenderInfo CalculateScreenPosition(
            TileInfo tile,
            PointD pyramidalOrigin,
            double viewResolution,
            int level,
            ITileSchema schema)
        {
            // Get the actual tile size from schema (typically 256x256)
            int tileWidth = schema.GetTileWidth(level);
            int tileHeight = schema.GetTileHeight(level);

            // Convert tile extent to pixel coordinates
            var pixelExtent = OpenSlideGTK.ExtentEx.WorldToPixelInvertedY(tile.Extent, viewResolution);

            // Use approach 1: MinY with positive origin
            double tileLeftPx = pixelExtent.MinX;
            double tileTopPx = pixelExtent.MinY;
            double viewXPx = pyramidalOrigin.X;
            double viewYPx = pyramidalOrigin.Y;

            float screenX = (float)(tileLeftPx - viewXPx);
            float screenY = (float)(tileTopPx - viewYPx);

            // KEY FIX: Use the extent dimensions directly, not fixed tile size
            // The extent already accounts for any edge tiles that might be smaller
            float screenWidth = (float)pixelExtent.Width;
            float screenHeight = (float)pixelExtent.Height;

            // Comprehensive debug logging
            // Console.WriteLine(...) // Reduced noise for production

            return new TileRenderInfo(tile.Index, screenX, screenY, screenWidth, screenHeight);
        }

        public int CurrentLevel => _currentLevel;
        public int CachedTextureCount => _uploadedTiles.Count;
    }
}