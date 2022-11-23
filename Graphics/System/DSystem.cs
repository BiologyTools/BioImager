using AForge;
using SharpDX.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Bio.Graphics
{
    public class DSystem                    // 120 lines
    {
        // Properties
        public DSystemConfiguration Configuration { get; private set; }
        public DInput Input { get; private set; }
        public DGraphics Graphics { get; private set; }

        // Constructor
        public DSystem() { }
        // Methods
        public virtual bool Initialize(string title, int width, int height, bool vSync, bool fullScreen, IntPtr handle)
        {
            bool result = false;

            if (Configuration == null)
                Configuration = new DSystemConfiguration(title, width, height, fullScreen, vSync);
            if (Input == null)
            {
                Input = new DInput();
                Input.Initialize();
            }
            if (Graphics == null)
            {
                Graphics = new DGraphics();
                result = Graphics.Initialize(Configuration, handle, ImageView.SelectedImage);
            }
            return result;
        }
        public bool Frame(IntRange r, IntRange g, IntRange b)
        {
            // Check if the user pressed escape and wants to exit the application.
            if (Input.IsKeyDown(Keys.Escape))
                return false;
            // Do the frame processing for the graphics object.
            return Graphics.Frame(r,g,b);
        }
        public void ShutDown()
        {
            Graphics?.ShutDown();
            Graphics = null;
            Input = null;
            Configuration = null;
        }
    }
}