using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;

namespace BioImager
{
    public class DDX11                  // 270 lines
    {
        // Properties.
        private bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        private SwapChain SwapChain { get; set; }
        public SharpDX.Direct3D11.Device Device { get; private set; }
        public DeviceContext DeviceContext { get; private set; }
        private RenderTargetView RenderTargetView { get; set; }
        private Texture2D DepthStencilBuffer { get; set; }
        public DepthStencilState DepthStencilState { get; set; }
        private DepthStencilView DepthStencilView { get; set; }
        private RasterizerState RasterState { get; set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; set; }

        // Constructor
        public DDX11() { }

        // Methods
        public bool Initialize(DSystemConfiguration configuration, IntPtr windowHandle)
        {
            try
            {
                // Store the vsync setting.
                VerticalSyncEnabled = DSystemConfiguration.VerticalSyncEnabled;

                // Create a DirectX graphics interface factory.
                var factory = new Factory1();

                // Use the factory to create an adapter for the primary graphics interface (video card).
                var adapter = factory.GetAdapter1(0);

                // Get the primary adapter output (monitor).
                var monitor = adapter.GetOutput(0);

                // Get modes that fit the DXGI_FORMAT_R8G8B8A8_UNORM display format for the adapter output (monitor).
                var modes = monitor.GetDisplayModeList(Format.R8G8B8A8_UNorm, DisplayModeEnumerationFlags.Interlaced);

                // Now go through all the display modes and find the one that matches the screen width and height.
                // When a match is found store the the refresh rate for that monitor, if vertical sync is enabled. 
                // Otherwise we use maximum refresh rate.
                var rational = new Rational(0, 1);
                if (VerticalSyncEnabled)
                {
                    foreach (var mode in modes)
                    {
                        if (mode.Width == configuration.Width && mode.Height == configuration.Height)
                        {
                            rational = new Rational(mode.RefreshRate.Numerator, mode.RefreshRate.Denominator);
                            break;
                        }
                    }
                }

                // Get the adapter (video card) description.
                var adapterDescription = adapter.Description;

                // Store the dedicated video card memory in megabytes.
                //VideoCardMemory = adapterDescription.DedicatedVideoMemory >> 10 >> 10;

                // Convert the name of the video card to a character array and store it.
                VideoCardDescription = adapterDescription.Description.Trim('\0');

                // Release the adapter output.
                monitor.Dispose();
                // Release the adapter.
                adapter.Dispose();
                // Release the factory.
                factory.Dispose();

                // Initialize the swap chain description.
                var swapChainDesc = new SwapChainDescription()
                {
                    // Set to a single back buffer.
                    BufferCount = 1,
                    // Set the width and height of the back buffer.
                    ModeDescription = new ModeDescription(configuration.Width, configuration.Height, rational, Format.R8G8B8A8_UNorm),
                    // Set the usage of the back buffer.
                    Usage = Usage.RenderTargetOutput,
                    // Set the handle for the window to render to.
                    OutputHandle = windowHandle,
                    // Turn multisampling off.
                    SampleDescription = new SampleDescription(1, 0),
                    // Set to full screen or windowed mode.
                    IsWindowed = !DSystemConfiguration.FullScreen,
                    // Don't set the advanced flags.
                    Flags = SwapChainFlags.None,
                    // Discard the back buffer content after presenting.
                    SwapEffect = SwapEffect.Discard
                };

                // Create the swap chain, Direct3D device, and Direct3D device context.
                SharpDX.Direct3D11.Device device;
                SwapChain swapChain;
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None, swapChainDesc, out device, out swapChain);

                Device = device;
                SwapChain = swapChain;
                DeviceContext = device.ImmediateContext;

                // Get the pointer to the back buffer.
                var backBuffer = Texture2D.FromSwapChain<Texture2D>(SwapChain, 0);

                // Create the render target view with the back buffer pointer.
                RenderTargetView = new RenderTargetView(device, backBuffer);

                // Release pointer to the back buffer as we no longer need it.
                backBuffer.Dispose();

                // Initialize and set up the description of the depth buffer.
                Texture2DDescription depthBufferDesc = new Texture2DDescription()
                {
                    Width = configuration.Width,
                    Height = configuration.Height,
                    MipLevels = 1,
                    ArraySize = 1,
                    Format = Format.D24_UNorm_S8_UInt,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    BindFlags = BindFlags.DepthStencil,
                    CpuAccessFlags = CpuAccessFlags.None,
                    OptionFlags = ResourceOptionFlags.None
                };

                // Create the texture for the depth buffer using the filled out description.
                DepthStencilBuffer = new Texture2D(device, depthBufferDesc);

                // Initialize and set up the description of the stencil state.
                DepthStencilStateDescription depthStencilDesc = new DepthStencilStateDescription()
                {
                    IsDepthEnabled = true,
                    DepthWriteMask = DepthWriteMask.All,
                    DepthComparison = Comparison.Less,
                    IsStencilEnabled = true,
                    StencilReadMask = 0xFF,
                    StencilWriteMask = 0xFF,
                    // Stencil operation if pixel front-facing.
                    FrontFace = new DepthStencilOperationDescription()
                    {
                        FailOperation = StencilOperation.Keep,
                        DepthFailOperation = StencilOperation.Increment,
                        PassOperation = StencilOperation.Keep,
                        Comparison = Comparison.Always
                    },
                    // Stencil operation if pixel is back-facing.
                    BackFace = new DepthStencilOperationDescription()
                    {
                        FailOperation = StencilOperation.Keep,
                        DepthFailOperation = StencilOperation.Decrement,
                        PassOperation = StencilOperation.Keep,
                        Comparison = Comparison.Always
                    }
                };

                // Create the depth stencil state.
                DepthStencilState = new DepthStencilState(Device, depthStencilDesc);

                // Set the depth stencil state.
                DeviceContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);

                // Initialize and set up the depth stencil view.
                DepthStencilViewDescription depthStencilViewDesc = new DepthStencilViewDescription()
                {
                    Format = Format.D24_UNorm_S8_UInt,
                    Dimension = DepthStencilViewDimension.Texture2D,
                    Texture2D = new DepthStencilViewDescription.Texture2DResource()
                    {
                        MipSlice = 0
                    }
                };

                // Create the depth stencil view.
                DepthStencilView = new DepthStencilView(Device, DepthStencilBuffer, depthStencilViewDesc);

                // Bind the render target view and depth stencil buffer to the output render pipeline.
                DeviceContext.OutputMerger.SetTargets(DepthStencilView, RenderTargetView);

                // Setup the raster description which will determine how and what polygon will be drawn.
                RasterizerStateDescription rasterDesc = new RasterizerStateDescription()
                {
                    IsAntialiasedLineEnabled = false,
                    CullMode = CullMode.Back,
                    DepthBias = 0,
                    DepthBiasClamp = .0f,
                    IsDepthClipEnabled = false,
                    FillMode = FillMode.Solid,
                    IsFrontCounterClockwise = false,
                    IsMultisampleEnabled = false,
                    IsScissorEnabled = false,
                    SlopeScaledDepthBias = .0f
                };

                // Create the rasterizer state from the description we just filled out.
                RasterState = new RasterizerState(Device, rasterDesc);

                // Now set the rasterizer state.
                DeviceContext.Rasterizer.State = RasterState;

                // Setup and create the viewport for rendering.
                DeviceContext.Rasterizer.SetViewport(0, 0, configuration.Width, configuration.Height, 0, 1);

                // Setup and create the projection matrix.
                ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)configuration.Width / (float)configuration.Height), DSystemConfiguration.ScreenNear, DSystemConfiguration.ScreenDepth);

                // Initialize the world matrix to the identity matrix.
                WorldMatrix = Matrix.Identity;

                BlendStateDescription blendStateDescription = new BlendStateDescription
                {
                    AlphaToCoverageEnable = false,
                };

                blendStateDescription.RenderTarget[0].IsBlendEnabled = true;
                blendStateDescription.RenderTarget[0].SourceBlend = BlendOption.SourceAlpha;
                blendStateDescription.RenderTarget[0].DestinationBlend = BlendOption.InverseSourceAlpha;
                blendStateDescription.RenderTarget[0].BlendOperation = BlendOperation.Add;
                blendStateDescription.RenderTarget[0].SourceAlphaBlend = BlendOption.Zero;
                blendStateDescription.RenderTarget[0].DestinationAlphaBlend = BlendOption.Zero;
                blendStateDescription.RenderTarget[0].AlphaBlendOperation = BlendOperation.Add;
                blendStateDescription.RenderTarget[0].RenderTargetWriteMask = ColorWriteMaskFlags.All;

                DeviceContext.OutputMerger.BlendState = new BlendState(device, blendStateDescription);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            // Before shutting down set to windowed mode or when you release the swap chain it will throw an exception.
            SwapChain?.SetFullscreenState(false, null);

            // Dispose of all objects.
            RasterState?.Dispose();
            RasterState = null;
            DepthStencilView?.Dispose();
            DepthStencilView = null;
            DepthStencilState?.Dispose();
            DepthStencilState = null;
            DepthStencilBuffer?.Dispose();
            DepthStencilBuffer = null;
            RenderTargetView?.Dispose();
            RenderTargetView = null;
            DeviceContext?.Dispose();
            DeviceContext = null;
            Device?.Dispose();
            Device = null;
            SwapChain?.Dispose();
            SwapChain = null;
        }
        public void BeginScene(float red, float green, float blue, float alpha)
        {
            BeginScene(new Color4(red, green, blue, alpha));
        }
        private void BeginScene(Color4 givenColour)
        {
            // Clear the depth buffer.
            DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);

            // Clear the back buffer.
            DeviceContext.ClearRenderTargetView(RenderTargetView, givenColour);
        }
        public void EndScene()
        {
            // Present the back buffer to the screen since rendering is complete.
            if (VerticalSyncEnabled)
                SwapChain.Present(1, PresentFlags.None); // Lock to screen refresh rate.
            else
                SwapChain.Present(0, PresentFlags.None); // Present as fast as possible.
        }
    }
}