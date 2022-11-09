using Bio.Graphics;
using Bio.Graphics.DX.Input;
using Bio.Graphics.DX;
using SharpDX.Windows;
using System.Drawing;
using System.Windows.Forms;
using System;
using Bio;
using System.Collections.Generic;

namespace Bio.Graphics.DX
{
    public class DSystem                    // 119 lines
    {
        // Properties
        private RenderForm RenderForm { get; set; }
        public DSystemConfiguration Configuration { get; private set; }
        public Bio.Graphics.DX.Input.DInput Input { get; private set; }
        public Bio.Graphics.DX.Graphics.DGraphics Graphics { get; private set; }

        public List<BioImage> list = new List<BioImage>();
        // Constructor
        public DSystem() { }
        // Methods
        public virtual bool Initialize(string title, int width, int height, bool vSync, bool fullScreen, List<BioImage> imgs, IntPtr handle)
        {
            bool result = false;

            if (Configuration == null)
                Configuration = new DSystemConfiguration(title, width, height, fullScreen, vSync);

            // Initialize Window.
            //InitializeWindows(title);

            if (Input == null)
            {
                Input = new Bio.Graphics.DX.Input.DInput();
                Input.Initialize();
            }
            if (Graphics == null)
            {
                Graphics = new Bio.Graphics.DX.Graphics.DGraphics();
                result = Graphics.Initialize(Configuration, handle,imgs, 10);
            }

            return result;
        }
        public bool Frame(int x, int y, int ScreenWidth, int ScreenHeight, SharpDX.Matrix world)
        {
            // Check if the user pressed escape and wants to exit the application.
            if (Input.IsKeyDown(Keys.Escape))
                return false;

            // Do the frame processing for the graphics object.
            return Graphics.Frame(x,y, ScreenWidth, ScreenHeight, world);
        }
        public void ShutDown()
        {
            ShutdownWindows();

            Graphics?.ShutDown();
            Graphics = null;
            Input = null;
            Configuration = null;
        }
        private void ShutdownWindows()
        {
            RenderForm?.Dispose();
            RenderForm = null;
        }
    }
}