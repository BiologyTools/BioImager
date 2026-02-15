using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BruTile;
using OpenTK.Graphics.OpenGL;
using AForge;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using OpenTK.GLControl;

namespace BioImager
{
    
    public class SlideGLArea : GLControl
    {
        // ============================================================================
        // GL Resources
        // ============================================================================

        private bool _glInitialized = false;
        private int _vao;
        private int _vbo;
        private int _shaderProgram;

        // Uniform locations (cached for performance)
        private int _locPos;
        private int _locSize;
        private int _locViewportSize;
        private int _locTex;

        // ============================================================================
        // Texture Cache - tiles uploaded to GPU
        // ============================================================================

        private Dictionary<TileIndex, int> _textureCache = new();
        private const int MAX_CACHED_TEXTURES = 500;

        // ============================================================================
        // Rendering State
        // ============================================================================

        public List<TileRenderInfo> TilesToRender { get; } = new();
        public bool NeedsRedraw { get; private set; }

        // ============================================================================
        // Shaders
        // ============================================================================

        private const string VertexShaderSource = @"
#version 330 core

layout(location=0) in vec2 aPos;
layout(location=1) in vec2 aUV;

uniform vec2 pos;           // pixel-space top-left of tile
uniform vec2 size;          // pixel-space size of tile
uniform vec2 viewportSize;  // (width, height) in pixels

out vec2 uv;

void main()
{
    // aPos is in [0,1] x [0,1]
    vec2 pixelPos = aPos * size + pos;

    // Convert from pixel coords to NDC (-1..1), with Y flipped for GL
    vec2 ndc;
    ndc.x = (pixelPos.x / viewportSize.x) * 2.0 - 1.0;
    ndc.y = 1.0 - (pixelPos.y / viewportSize.y) * 2.0;

    gl_Position = vec4(ndc, 0.0, 1.0);
    uv = aUV;
}
";

        private const string FragmentShaderSource = @"
#version 330 core

in vec2 uv;
out vec4 FragColor;
uniform sampler2D tex;

void main()
{
    // Fix: Removed '1.0 - uv.y'. 
    // Image data is usually Top-Down. OpenGL Textures are Bottom-Up.
    // This implicit flip means UV(0,0) (Top-Left Quad) maps to Texture Bottom (Image Top).
    // This is the correct orientation for Top-Left origin rendering.
    FragColor = texture(tex, vec2(uv.x, uv.y));
}
";

        // ============================================================================
        // Construction
        // ============================================================================

        public SlideGLArea()
        {
            Paint += OnRealized;
            Disposed += SlideGLArea_Unrealized;
            Resize += OnResized;
        }

        private void SlideGLArea_Unrealized(object? sender, EventArgs e)
        {
            MakeCurrent();
            CleanupGL();
        }

        // ============================================================================
        // GLArea Lifecycle
        // ============================================================================

        private void OnRealized(object? sender, EventArgs e)
        {
            MakeCurrent();
            InitializeGL();
            _glInitialized = true;
        }

        private void OnResized(object? sender, EventArgs e)
        {
            if (!_glInitialized) return;
            MakeCurrent();
        }

        // ============================================================================
        // GL Initialization
        // ============================================================================

        private void InitializeGL()
        {
            // Compile shaders
            int vertexShader = CompileShader(ShaderType.VertexShader, VertexShaderSource);
            int fragmentShader = CompileShader(ShaderType.FragmentShader, FragmentShaderSource);

            _shaderProgram = GL.CreateProgram();
            GL.AttachShader(_shaderProgram, vertexShader);
            GL.AttachShader(_shaderProgram, fragmentShader);
            GL.LinkProgram(_shaderProgram);

            GL.GetProgram(_shaderProgram, GetProgramParameterName.LinkStatus, out int linkStatus);
            if (linkStatus == 0)
            {
                string infoLog = GL.GetProgramInfoLog(_shaderProgram);
                throw new Exception($"Shader link failed: {infoLog}");
            }

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Cache uniform locations
            _locPos = GL.GetUniformLocation(_shaderProgram, "pos");
            _locSize = GL.GetUniformLocation(_shaderProgram, "size");
            _locViewportSize = GL.GetUniformLocation(_shaderProgram, "viewportSize");
            _locTex = GL.GetUniformLocation(_shaderProgram, "tex");

            // Create quad VAO/VBO for tile rendering
            float[] quadVertices =
            {
                // pos    uv
                0, 0,     0, 0,
                1, 0,     1, 0,
                1, 1,     1, 1,

                0, 0,     0, 0,
                1, 1,     1, 1,
                0, 1,     0, 1
            };

            _vao = GL.GenVertexArray();
            _vbo = GL.GenBuffer();

            GL.BindVertexArray(_vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float),
                          quadVertices, BufferUsageHint.StaticDraw);

            // Position attribute
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            // UV attribute
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.BindVertexArray(0);
        }

        private int CompileShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int status);
            if (status == 0)
            {
                string infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Shader compilation failed ({type}): {infoLog}");
            }

            return shader;
        }

        private void CleanupGL()
        {
            if (_shaderProgram != 0)
            {
                GL.DeleteProgram(_shaderProgram);
                _shaderProgram = 0;
            }

            if (_vbo != 0)
            {
                GL.DeleteBuffer(_vbo);
                _vbo = 0;
            }

            if (_vao != 0)
            {
                GL.DeleteVertexArray(_vao);
                _vao = 0;
            }

            ClearTextureCache();
        }

        // ============================================================================
        // Rendering
        // ============================================================================

        public void OnRender()
        {
            if (!_glInitialized) return;
            MakeCurrent();
            int width = Width;
            int height = Height;
            if (width <= 0 || height <= 0) return;
            // Phase 1: Render tiles with OpenGL
            RenderTiles(width, height);
        }

        private void RenderTiles(int width, int height)
        {
            // Multiply by ScaleFactor to handle High-DPI screens correctly
            double scale = DeviceDpi / 96f;
            GL.Viewport(0, 0, (int)(width * scale), (int)(height * scale));
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (TilesToRender.Count == 0)
            {
                Console.WriteLine("WARNING: No tiles to render!");
                return;
            }

            Console.WriteLine($"Rendering {TilesToRender.Count} tiles at viewport {width}x{height}");

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.UseProgram(_shaderProgram);
            GL.BindVertexArray(_vao);

            // Set viewport size uniform (constant for all tiles this frame)
            GL.Uniform2(_locViewportSize, (float)width, (float)height);
            GL.Uniform1(_locTex, 0);

            GL.ActiveTexture(TextureUnit.Texture0);

            int renderedCount = 0;
            foreach (var tile in TilesToRender)
            {
                if (!_textureCache.TryGetValue(tile.Index, out int texId))
                {
                    Console.WriteLine($"WARNING: Tile {tile.Index} not in texture cache!");
                    continue;
                }

                GL.Uniform2(_locPos, tile.ScreenX, tile.ScreenY);
                GL.Uniform2(_locSize, tile.ScreenWidth, tile.ScreenHeight);

                Console.WriteLine($"Rendering tile {tile.Index}: pos=({tile.ScreenX}, {tile.ScreenY}), size=({tile.ScreenWidth}, {tile.ScreenHeight})");

                GL.BindTexture(TextureTarget.Texture2D, texId);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                renderedCount++;
            }

            Console.WriteLine($"Successfully rendered {renderedCount} tiles");

            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.Disable(EnableCap.Blend);
        }
        /*
        private void RenderSkiaOverlay(int width, int height)
        {
            if (_skSurface == null || OnSkiaRender == null)
            {
                InitializeSkia();
            }
            var canvas = _skSurface.Canvas;
            // Fire event for annotation drawing
            OnSkiaRender?.Invoke(canvas, width, height);
            _grContext.Flush();
        }
        */
        // ============================================================================
        // Texture Management
        // ============================================================================

        /// <summary>
        /// Upload a tile texture to the GPU. Call from the main thread.
        /// </summary>
        public void UploadTileTexture(TileIndex index, byte[] pixelData, int tileWidth, int tileHeight)
        {
            if (!_glInitialized)
            {
                Console.WriteLine("WARNING: GL not initialized, cannot upload texture");
                return;
            }

            if (pixelData == null || pixelData.Length == 0)
            {
                Console.WriteLine($"WARNING: Empty pixel data for tile {index}");
                return;
            }

            MakeCurrent();

            // Check if already cached
            if (_textureCache.ContainsKey(index))
            {
                Console.WriteLine($"Tile {index} already cached");
                return;
            }

            // Evict old textures if cache is full
            if (_textureCache.Count >= MAX_CACHED_TEXTURES)
            {
                EvictOldestTextures(MAX_CACHED_TEXTURES / 4);
            }

            // Create and upload texture
            int tex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tex);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            // CRITICAL FIX: Use Rgba instead of Bgra for internal format consistency
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                          tileWidth, tileHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixelData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            _textureCache[index] = tex;
            Console.WriteLine($"Uploaded tile {index} as texture {tex} ({tileWidth}x{tileHeight}, {pixelData.Length} bytes)");
        }

        /// <summary>
        /// Check if a tile texture is already in the GPU cache.
        /// </summary>
        public bool HasTileTexture(TileIndex index)
        {
            return _textureCache.ContainsKey(index);
        }

        /// <summary>
        /// Release a specific tile texture.
        /// </summary>
        public void ReleaseTileTexture(TileIndex index)
        {
            if (_textureCache.TryGetValue(index, out int tex))
            {
                MakeCurrent();
                GL.DeleteTexture(tex);
                _textureCache.Remove(index);
            }
        }

        /// <summary>
        /// Release all textures for a specific pyramid level.
        /// </summary>
        public void ReleaseLevelTextures(int level)
        {
            if (!_glInitialized) return;

            MakeCurrent();

            var toRemove = _textureCache.Where(kvp => kvp.Key.Level == level).ToList();
            foreach (var kvp in toRemove)
            {
                GL.DeleteTexture(kvp.Value);
                _textureCache.Remove(kvp.Key);
            }
        }

        /// <summary>
        /// Clear all cached textures.
        /// </summary>
        public void ClearTextureCache()
        {
            if (!_glInitialized) return;

            MakeCurrent();

            foreach (var tex in _textureCache.Values)
            {
                GL.DeleteTexture(tex);
            }
            _textureCache.Clear();
        }

        private void EvictOldestTextures(int count)
        {
            // Simple FIFO eviction - could be improved with LRU tracking
            var toRemove = _textureCache.Take(count).ToList();
            foreach (var kvp in toRemove)
            {
                GL.DeleteTexture(kvp.Value);
                _textureCache.Remove(kvp.Key);
            }
        }

        // ============================================================================
        // Public API
        // ============================================================================

        /// <summary>
        /// Request a redraw of the GLArea.
        /// </summary>
        public void RequestRedraw()
        {
            NeedsRedraw = true;
            Invalidate();
        }

        /// <summary>
        /// Prepare tiles for rendering. Call before RequestRedraw().
        /// </summary>
        public void SetTilesToRender(IEnumerable<TileRenderInfo> tiles)
        {
            TilesToRender.Clear();
            TilesToRender.AddRange(tiles);
            Console.WriteLine($"Set {TilesToRender.Count} tiles to render");
        }

        /// <summary>
        /// Read pixels from the current framebuffer (for export/save operations).
        /// This is slow - only use for export, not display.
        /// </summary>
        public byte[] ReadPixels()
        {
            if (!_glInitialized) return null;

            MakeCurrent();

            int width = Width;
            int height = Height;

            byte[] pixels = new byte[width * height * 4];
            GL.ReadPixels(0, 0, width, height, PixelFormat.Rgba, PixelType.UnsignedByte, pixels);

            return pixels;
        }
    }

    // ============================================================================
    // Supporting Types
    // ============================================================================

    /// <summary>
    /// Information needed to render a single tile.
    /// </summary>
    public struct TileRenderInfo
    {
        public TileIndex Index;

        // Screen-space position and size in pixels
        public float ScreenX;
        public float ScreenY;
        public float ScreenWidth;
        public float ScreenHeight;

        public TileRenderInfo(TileIndex index, float x, float y, float w, float h)
        {
            Index = index;
            ScreenX = x;
            ScreenY = y;
            ScreenWidth = w;
            ScreenHeight = h;
        }
    }
}