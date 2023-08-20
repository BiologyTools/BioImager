using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using System.Drawing.Imaging;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Bitmap = SharpDX.Direct2D1.Bitmap;
using PixelFormat = SharpDX.Direct2D1.PixelFormat;

namespace Bio
{
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration() : this("Bio")
        {
            WaitVerticalBlanking = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration(string title) : this(title, 800, 600)
        {
            WaitVerticalBlanking = true;
        }

        public Configuration(string title, int width, int height)
        {
            Title = title;
            Width = width;
            Height = height;
            WaitVerticalBlanking = true;
        }

        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [wait vertical blanking].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [wait vertical blanking]; otherwise, <c>false</c>.
        /// </value>
        public bool WaitVerticalBlanking
        {
            get; set;
        }
    }
    class DBitmap
    {
        private Bitmap _bitmap;

        /// <summary>
        /// Loads a Direct2D Bitmap from a file using System.Drawing.Image.FromFile(...)
        /// </summary>
        /// <param name="renderTarget">The render target.</param>
        /// <param name="file">The file.</param>
        /// <returns>A D2D1 Bitmap</returns>
        public static Bitmap FromImage(RenderTarget renderTarget, AForge.Bitmap image)
        {
            var bitmapProperties = new BitmapProperties(new PixelFormat(Format.R8G8B8A8_UNorm, AlphaMode.Ignore));
            var size = new Size2(image.Width, image.Height);
            return new Bitmap(renderTarget, size, new DataPointer(image.RGBData, image.Width * 4 * image.Height), image.Width * 4, bitmapProperties);
        }
        public void Initialize(Configuration configuration, RenderTarget renderTarget2, AForge.Bitmap bf)
        {
            _bitmap = FromImage(renderTarget2, bf);
        }
    }
}
