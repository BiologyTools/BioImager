using SharpDX.Direct2D1;
using SharpDX.DXGI;

using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Factory = SharpDX.Direct2D1.Factory;

namespace Bio
{
    public class Direct2D : IDisposable
    {
        public Factory Factory2D { get; private set; }
        public SharpDX.DirectWrite.Factory FactoryDWrite { get; private set; }
        public WindowRenderTarget RenderTarget2D { get; private set; }
        public SolidColorBrush SceneColorBrush { get; private set; }
        HwndRenderTargetProperties properties = new HwndRenderTargetProperties();
        public void Initialize(Configuration configuration, IntPtr handle)
        {
            Factory2D = new SharpDX.Direct2D1.Factory();
            FactoryDWrite = new SharpDX.DirectWrite.Factory();
            properties.Hwnd = handle;
            properties.PixelSize.Height = configuration.Height;
            properties.PixelSize.Width = configuration.Width;
            properties.PresentOptions = PresentOptions.Immediately;
            RenderTarget2D = new WindowRenderTarget(Factory2D, new RenderTargetProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore)), properties);
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;
            SceneColorBrush = new SolidColorBrush(RenderTarget2D, SharpDX.Color.White);
        }
        public void Update(Configuration configuration, IntPtr handle)
        {

            properties.Hwnd = handle;
            properties.PixelSize.Height = configuration.Height;
            properties.PixelSize.Width = configuration.Width;
            properties.PresentOptions = PresentOptions.Immediately;
            RenderTarget2D.Resize(properties.PixelSize);
            RenderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

        }
        bool draw = false;
        public void BeginDraw()
        {
            RenderTarget2D.BeginDraw();
            draw = true;
        }
        public void EndDraw()
        {
            long t1, t2;
            RenderTarget2D.TryEndDraw(out t1, out t2);
            draw = false;
        }
        public void Dispose()
        {
            Factory2D.Dispose();
            RenderTarget2D.Dispose();
            SceneColorBrush.Dispose();
            FactoryDWrite.Dispose();
        }

    }
}
