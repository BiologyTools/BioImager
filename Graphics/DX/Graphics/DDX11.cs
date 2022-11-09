using Bio.Graphics.DX;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;

namespace Bio.Graphics.DX.Graphics
{
    public class DDX11                  // 316 lines
    {
        // Properties.
        private bool VerticalSyncEnabled { get; set; }
        public int VideoCardMemory { get; private set; }
        public string VideoCardDescription { get; private set; }
        private SwapChain SwapChain { get; set; }
        public SharpDX.Direct3D11.Device Device { get; private set; }
        public DeviceContext DeviceContext { get; private set; }
		public SharpDX.Direct2D1.Device Device2D { get; private set; }
		public SharpDX.Direct2D1.DeviceContext DeviceContext2D { get; private set; }
		private RenderTargetView RenderTargetView { get; set; }
		private SharpDX.Direct2D1.RenderTarget RenderTarget { get; set; }
		private Texture2D DepthStencilBuffer { get; set; }
        public DepthStencilState DepthStencilState { get; set; }
        private DepthStencilView DepthStencilView { get; set; }
        private RasterizerState RasterState { get; set; }
        public Matrix ProjectionMatrix { get; private set; }
        public Matrix WorldMatrix { get; private set; }
        public Matrix OrthoMatrix { get; private set; }
        public DepthStencilState DepthDisabledStencilState { get; private set; }

        // Constructor
        public DDX11() { }

        public bool Initialize(Bio.Graphics.DX.DSystemConfiguration configuration, IntPtr windowHandle)
		{
			try
			{
				// Store the vsync setting.
				VerticalSyncEnabled = Bio.Graphics.DX.DSystemConfiguration.VerticalSyncEnabled;

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

				// Store the dedicated video card memory in megabytes. This throws an error.
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
					IsWindowed = !Bio.Graphics.DX.DSystemConfiguration.FullScreen,
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
				var depthBufferDesc = new Texture2DDescription()
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
				var depthStencilDesc = new DepthStencilStateDescription()
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
				var depthStencilViewDesc = new DepthStencilViewDescription()
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
				var rasterDesc = new RasterizerStateDescription()
				{
					IsAntialiasedLineEnabled = false,
					CullMode = CullMode.Back,
					DepthBias = 0,
					DepthBiasClamp = .0f,
					IsDepthClipEnabled = true,
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
                ProjectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)configuration.Width / (float)configuration.Height), Bio.Graphics.DX.DSystemConfiguration.ScreenNear, Bio.Graphics.DX.DSystemConfiguration.ScreenDepth);

				// Initialize the world matrix to the identity matrix.
				WorldMatrix = Matrix.Identity;

				// Create an orthographic projection matrix for 2D rendering.
				OrthoMatrix = Matrix.OrthoLH(configuration.Width, configuration.Height, Bio.Graphics.DX.DSystemConfiguration.ScreenNear, Bio.Graphics.DX.DSystemConfiguration.ScreenDepth);

				// Now create a second depth stencil state which turns off the Z buffer for 2D rendering. Added in Tutorial 11
				// The difference is that DepthEnable is set to false.
				// All other parameters are the same as the other depth stencil state.
				var depthDisabledStencilDesc = new DepthStencilStateDescription()
				{
					IsDepthEnabled = false,
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
				DepthDisabledStencilState = new DepthStencilState(Device, depthDisabledStencilDesc);


				SharpDX.DXGI.Device2 dxgiDevice2 = device.QueryInterface<SharpDX.DXGI.Device2>();
				SharpDX.DXGI.Adapter dxgiAdapter = dxgiDevice2.Adapter;
				SharpDX.DXGI.Factory2 dxgiFactory2 = dxgiAdapter.GetParent<SharpDX.DXGI.Factory2>();

				Device2D = new SharpDX.Direct2D1.Device(dxgiDevice2);
				DeviceContext2D = new SharpDX.Direct2D1.DeviceContext(Device2D, SharpDX.Direct2D1.DeviceContextOptions.None);

				// Description for our swap chain settings.
				SwapChainDescription1 description = new SwapChainDescription1()
				{
					// 0 means to use automatic buffer sizing.
					Width = 0,
					Height = 0,
					// 32 bit RGBA color.
					Format = Format.B8G8R8A8_UNorm,
					// No stereo (3D) display.
					Stereo = false,
					// No multisampling.
					SampleDescription = new SampleDescription(1, 0),
					// Use the swap chain as a render target.
					Usage = Usage.RenderTargetOutput,
					// Enable double buffering to prevent flickering.
					BufferCount = 2,
					// No scaling.
					Scaling = Scaling.None,
					// Flip between both buffers.
					SwapEffect = SwapEffect.FlipSequential,
				};

				// Generate a swap chain for our window based on the specified description.
				//swapChain = dxgiFactory2.Adapters.(device, new ComObject(window), ref description, null);
				// Create the texture and render target that will hold our backbuffer.
				//Texture2D backBufferTexture = Texture2D.FromSwapChain<Texture2D>(swapChain, 0);
				//backBuffer = new RenderTargetView(device, backBufferTexture);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}
		}
        public void ShutDown()
        {
            // Before shutting down set to windowed mode or when you release the swap chain it will throw an exception.
            SwapChain?.SetFullscreenState(false, null);

            // Dispose of all objects.
            DepthDisabledStencilState?.Dispose();
            DepthDisabledStencilState = null;
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

			DeviceContext2D.Clear(new SharpDX.Mathematics.Interop.RawColor4(1, 1, 1, 1));

			SharpDX.Direct2D1.SolidColorBrush brush = new SharpDX.Direct2D1.SolidColorBrush(RenderTarget,new SharpDX.Mathematics.Interop.RawColor4(1.0f,0.0f,0.0f,1.0f));
			DeviceContext2D.FillRectangle(new SharpDX.Mathematics.Interop.RawRectangleF(0, 0, 0, 0), brush);

        }
        public void EndScene()
        {
            // Present the back buffer to the screen since rendering is complete.
            if (VerticalSyncEnabled)
                SwapChain.Present(1, PresentFlags.None); // Lock to screen refresh rate.
            else
                SwapChain.Present(0, PresentFlags.None); // Present as fast as possible.
        }
        public void TurnZBufferOn()
		{
			DeviceContext.OutputMerger.SetDepthStencilState(DepthStencilState, 1);
		}
		public void TurnZBufferOff()
		{
			DeviceContext.OutputMerger.SetDepthStencilState(DepthDisabledStencilState, 1);
		}
    }
    /// <summary>
    /// The view provider class that will handle all the view operations (update/draw).
    /// </summary>
    internal class MyViewProvider
    {
        private IntPtr window;
        private SharpDX.Direct3D11.Device1 device;
        private SharpDX.Direct3D11.DeviceContext1 d3dContext;
        private SharpDX.Direct2D1.DeviceContext d2dContext;
        private SwapChain1 swapChain;
        private SharpDX.Direct2D1.Bitmap1 d2dTarget;

        private SolidColorBrush solidBrush;
        private LinearGradientBrush linearGradientBrush;
        private RadialGradientBrush radialGradientBrush;

        /// <summary>
        /// This function is called before SetWindow, so we can't do much yet.
        /// </summary>
        /// <param name="applicationView"></param>
        public void Initialize()
        {
        }

        /// <summary>
        /// Now that we have a CoreWindow object, the DirectX device/context can be created.
        /// </summary>
        /// <param name="entryPoint"></param>
        public void Load(string entryPoint)
        {
            // Get the default hardware device and enable debugging. Don't care about the available feature level.
            // DeviceCreationFlags.BgraSupport must be enabled to allow Direct2D interop.
            SharpDX.Direct3D11.Device defaultDevice = new SharpDX.Direct3D11.Device(DriverType.Hardware, DeviceCreationFlags.Debug | DeviceCreationFlags.BgraSupport);

            // Query the default device for the supported device and context interfaces.
            device = defaultDevice.QueryInterface<SharpDX.Direct3D11.Device1>();
            d3dContext = device.ImmediateContext.QueryInterface<SharpDX.Direct3D11.DeviceContext1>();

            // Query for the adapter and more advanced DXGI objects.
            SharpDX.DXGI.Device2 dxgiDevice2 = device.QueryInterface<SharpDX.DXGI.Device2>();
            SharpDX.DXGI.Adapter dxgiAdapter = dxgiDevice2.Adapter;
            SharpDX.DXGI.Factory2 dxgiFactory2 = dxgiAdapter.GetParent<SharpDX.DXGI.Factory2>();

            // Description for our swap chain settings.
            SwapChainDescription1 description = new SwapChainDescription1()
            {
                // 0 means to use automatic buffer sizing.
                Width = 0,
                Height = 0,
                // 32 bit RGBA color.
                Format = Format.B8G8R8A8_UNorm,
                // No stereo (3D) display.
                Stereo = false,
                // No multisampling.
                SampleDescription = new SampleDescription(1, 0),
                // Use the swap chain as a render target.
                Usage = Usage.RenderTargetOutput,
                // Enable double buffering to prevent flickering.
                BufferCount = 2,
                // No scaling.
                Scaling = Scaling.None,
                // Flip between both buffers.
                SwapEffect = SwapEffect.FlipSequential,
            };

            // Generate a swap chain for our window based on the specified description.
            swapChain = dxgiFactory2.CreateSwapChainForCoreWindow(device, new ComObject(window), ref description, null);

            // Get the default Direct2D device and create a context.
            SharpDX.Direct2D1.Device d2dDevice = new SharpDX.Direct2D1.Device(dxgiDevice2);
            d2dContext = new SharpDX.Direct2D1.DeviceContext(d2dDevice, SharpDX.Direct2D1.DeviceContextOptions.None);

            // Specify the properties for the bitmap that we will use as the target of our Direct2D operations.
            // We want a 32-bit BGRA surface with premultiplied alpha.
            BitmapProperties1 properties = new BitmapProperties1(new PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied),
                DisplayProperties.LogicalDpi, DisplayProperties.LogicalDpi, BitmapOptions.Target | BitmapOptions.CannotDraw);

            // Get the default surface as a backbuffer and create the Bitmap1 that will hold the Direct2D drawing target.
            Surface backBuffer = swapChain.GetBackBuffer<Surface>(0);
            d2dTarget = new Bitmap1(d2dContext, backBuffer, properties);

            // Create a solid color brush.
            solidBrush = new SolidColorBrush(d2dContext, Color.Coral);

            // Create a linear gradient brush.
            // Note that the StartPoint and EndPoint values are set as absolute coordinates of the surface you are drawing to,
            // NOT the geometry we will apply the brush.
            linearGradientBrush = new LinearGradientBrush(d2dContext, new LinearGradientBrushProperties()
            {
                StartPoint = new Vector2(50, 0),
                EndPoint = new Vector2(450, 0),
            },
                new GradientStopCollection(d2dContext, new GradientStop[]
                    {
                        new GradientStop()
                        {
                            Color = Color.Blue,
                            Position = 0,
                        },
                        new GradientStop()
                        {
                            Color = Color.Green,
                            Position = 1,
                        }
                    }));

            // Create a radial gradient brush.
            // The center is specified in absolute coordinates, too.
            radialGradientBrush = new RadialGradientBrush(d2dContext, new RadialGradientBrushProperties()
            {
                Center = new Vector2(250, 525),
                RadiusX = 100,
                RadiusY = 100,
            },
                new GradientStopCollection(d2dContext, new GradientStop[]
                {
                        new GradientStop()
                        {
                            Color = Color.Yellow,
                            Position = 0,
                        },
                        new GradientStop()
                        {
                            Color = Color.Red,
                            Position = 1,
                        }
                }));

        }

        /// <summary>
        /// Run our application until the user quits.
        /// </summary>
        public void Run()
        {
            // Set the Direct2D drawing target.
            d2dContext.Target = d2dTarget;

            // Clear the target and draw some geometry with the brushes we created. 
            d2dContext.BeginDraw();
            d2dContext.Clear(Color.CornflowerBlue);
            d2dContext.FillRectangle(new RectangleF(50, 50, 450, 150), solidBrush);
            d2dContext.FillRoundedRectangle(new RoundedRectangle()
            {
                Rect = new RectangleF(50, 250, 450, 150),
                RadiusX = 10,
                RadiusY = 10
            }, linearGradientBrush);
            d2dContext.FillEllipse(new Ellipse(new Vector2(250, 525), 100, 100), radialGradientBrush);
            d2dContext.EndDraw();

            // Present the current buffer to the screen.
            swapChain.Present(1, PresentFlags.None);
            
        }

        /// <summary>
        /// Dispose all the created objects.
        /// </summary>
        public void Uninitialize()
        {
            radialGradientBrush.Dispose();
            linearGradientBrush.Dispose();
            solidBrush.Dispose();
            swapChain.Dispose();
            d2dTarget.Dispose();
            d3dContext.Dispose();
            d2dContext.Dispose();
            device.Dispose();
        }
    }
}