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
        // GL Ready Event — fires once after GL context is confirmed working
        // ============================================================================

        /// <summary>
        /// Fires exactly once after the GL context has been successfully initialized.
        /// Subscribe to this from ImageView to trigger the first tile load at a point
        /// where GL texture uploads are guaranteed to work.
        /// </summary>
        public event EventHandler GLReady;
        private bool _glReadyFired = false;

        /// <summary>
        /// True after the GL context has been fully initialized and is ready
        /// for texture uploads and rendering.
        /// </summary>
        public bool IsGLReady => _glInitialized;

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
    // Image data is usually Top-Down. OpenGL Textures are Bottom-Up.
    // This implicit flip means UV(0,0) (Top-Left Quad) maps to Texture Bottom (Image Top).
    // This is the correct orientation for Top-Left origin rendering.
    FragColor = texture(tex, vec2(uv.x, uv.y));
}
";

        // ============================================================================
        // Construction & Lifecycle
        // ============================================================================

        public SlideGLArea()
        {
            Paint += OnPaint;
            Disposed += OnDisposed;
            Resize += OnResized;
        }

        private void OnDisposed(object? sender, EventArgs e)
        {
            MakeCurrent();
            CleanupGL();
        }

        /// <summary>
        /// Unified paint handler: ensures GL is initialized once, then renders and swaps.
        /// </summary>
        private void OnPaint(object? sender, EventArgs e)
        {
            MakeCurrent();

            if (!_glInitialized)
            {
                InitializeGL();

                // Fire GLReady exactly once — this is the safe moment for the first tile load
                if (_glInitialized && !_glReadyFired)
                {
                    _glReadyFired = true;
                    Console.WriteLine("[SlideGLArea] GL initialized. Firing GLReady event.");
                    GLReady?.Invoke(this, EventArgs.Empty);
                }
            }

            RenderFrame();
            SwapBuffers();
            NeedsRedraw = false;
        }

        private void OnResized(object? sender, EventArgs e)
        {
            if (!_glInitialized) return;
            MakeCurrent();
            Invalidate();
        }

        // ============================================================================
        // GL Initialization (called once)
        // ============================================================================

        private void InitializeGL()
        {
            if (_glInitialized) return;

            try
            {
                // Compile and link shader program
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
                    Console.WriteLine($"[SlideGLArea] Shader link FAILED: {infoLog}");
                    return;
                }

                GL.DeleteShader(vertexShader);
                GL.DeleteShader(fragmentShader);

                // Cache uniform locations
                _locPos = GL.GetUniformLocation(_shaderProgram, "pos");
                _locSize = GL.GetUniformLocation(_shaderProgram, "size");
                _locViewportSize = GL.GetUniformLocation(_shaderProgram, "viewportSize");
                _locTex = GL.GetUniformLocation(_shaderProgram, "tex");

                Console.WriteLine($"[SlideGLArea] Uniform locations: pos={_locPos} size={_locSize} viewport={_locViewportSize} tex={_locTex}");

                // Build the unit-quad VAO/VBO shared by all tile draws
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

                // Position attribute (location = 0)
                GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);

                // UV attribute (location = 1)
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), 2 * sizeof(float));
                GL.EnableVertexAttribArray(1);

                GL.BindVertexArray(0);

                // Verify no GL errors accumulated during setup
                var err = GL.GetError();
                if (err != ErrorCode.NoError)
                    Console.WriteLine($"[SlideGLArea] GL error after init: {err}");

                _glInitialized = true;
                Console.WriteLine($"[SlideGLArea] GL initialization complete. VAO={_vao} VBO={_vbo} Program={_shaderProgram}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SlideGLArea] GL initialization FAILED: {ex.Message}");
            }
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
                Console.WriteLine($"[SlideGLArea] Shader compile FAILED ({type}): {infoLog}");
                throw new Exception($"Shader compilation failed ({type}): {infoLog}");
            }

            Console.WriteLine($"[SlideGLArea] Shader compiled OK ({type})");
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
            _glInitialized = false;
        }

        // ============================================================================
        // Rendering
        // ============================================================================

        private void RenderFrame()
        {
            int width = Width;
            int height = Height;
            if (width <= 0 || height <= 0) return;

            // Handle High-DPI: viewport is in physical pixels, uniforms in logical pixels
            double scale = DeviceDpi / 96.0;
            GL.Viewport(0, 0, (int)(width * scale), (int)(height * scale));
            GL.ClearColor(0.2f, 0.2f, 0.2f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (TilesToRender.Count == 0)
                return;

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.UseProgram(_shaderProgram);
            GL.BindVertexArray(_vao);

            // Viewport size uniform is constant for all tiles this frame
            GL.Uniform2(_locViewportSize, (float)width, (float)height);
            GL.Uniform1(_locTex, 0);
            GL.ActiveTexture(TextureUnit.Texture0);

            int rendered = 0;
            int skipped = 0;
            foreach (var tile in TilesToRender)
            {
                if (!_textureCache.TryGetValue(tile.Index, out int texId))
                {
                    skipped++;
                    continue;
                }

                GL.Uniform2(_locPos, tile.ScreenX, tile.ScreenY);
                GL.Uniform2(_locSize, tile.ScreenWidth, tile.ScreenHeight);

                GL.BindTexture(TextureTarget.Texture2D, texId);
                GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
                rendered++;
            }

            // Diagnostic: log once when tiles first appear (or fail to appear)
            if (rendered == 0 && skipped > 0)
                Console.WriteLine($"[SlideGLArea] RenderFrame: {TilesToRender.Count} tiles to render, but ALL {skipped} skipped (no texture in cache).");
            else if (rendered > 0)
                Console.WriteLine($"[SlideGLArea] RenderFrame: rendered {rendered} tiles, skipped {skipped}.");

            GL.BindVertexArray(0);
            GL.UseProgram(0);
            GL.Disable(EnableCap.Blend);
        }

        // ============================================================================
        // Texture Management
        // ============================================================================

        /// <summary>
        /// Upload a tile texture to the GPU. Call from the main thread.
        /// Returns true if upload succeeded.
        /// </summary>
        public bool UploadTileTexture(TileIndex index, byte[] pixelData, int tileWidth, int tileHeight)
        {
            if (pixelData == null || pixelData.Length == 0)
                return false;

            // Guard: don't attempt GL operations if the context isn't ready
            if (!IsHandleCreated)
            {
                Console.WriteLine($"[SlideGLArea] UploadTileTexture skipped — control handle not created yet.");
                return false;
            }

            try
            {
                MakeCurrent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[SlideGLArea] MakeCurrent failed in UploadTileTexture: {ex.Message}");
                return false;
            }

            if (!_glInitialized)
                InitializeGL();

            if (!_glInitialized)
            {
                Console.WriteLine($"[SlideGLArea] UploadTileTexture skipped — GL init failed.");
                return false;
            }

            // Already cached — nothing to do
            if (_textureCache.ContainsKey(index))
                return true;

            // Evict old textures if cache is full
            if (_textureCache.Count >= MAX_CACHED_TEXTURES)
                EvictOldestTextures(MAX_CACHED_TEXTURES / 4);

            // Validate pixel data size
            int expectedBytes = tileWidth * tileHeight * 4;
            if (pixelData.Length < expectedBytes)
            {
                Console.WriteLine($"[SlideGLArea] UploadTileTexture: pixel data too small. Expected {expectedBytes} bytes for {tileWidth}x{tileHeight}, got {pixelData.Length}.");
                return false;
            }

            // Create and upload texture
            int tex = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, tex);
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                          tileWidth, tileHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pixelData);

            var glErr = GL.GetError();
            if (glErr != ErrorCode.NoError)
            {
                Console.WriteLine($"[SlideGLArea] GL error after TexImage2D ({tileWidth}x{tileHeight}): {glErr}");
                GL.DeleteTexture(tex);
                return false;
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.BindTexture(TextureTarget.Texture2D, 0);
            _textureCache[index] = tex;
            return true;
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
                GL.DeleteTexture(tex);

            _textureCache.Clear();
        }

        private void EvictOldestTextures(int count)
        {
            // Simple FIFO eviction — could be improved with LRU tracking
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
        }

        /// <summary>
        /// Read pixels from the current framebuffer (for export/save operations).
        /// This is slow — only use for export, not display.
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