using Bio.Graphics.DX;
using SharpDX;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bio.Graphics.DX.Graphics
{
    public class DGraphics                  // 124 lines
    {
        // Properties
        private DDX11 D3D { get; set; }
        public SharpDX.Direct3D11.Device Device
        {
            get
            {
                return D3D.Device;
            }
        }
        public SharpDX.Direct3D11.DeviceContext DeviceContext
        {
            get
            {
                return D3D.DeviceContext;
            }
        }
        public SharpDX.Direct2D1.Device Device2D
        {
            get
            {
                return D3D.Device2D;
            }
        }
        public SharpDX.Direct2D1.DeviceContext DeviceContext2D
        {
            get
            {
                return D3D.DeviceContext2D;
            }
        }
        public DCamera Camera { get; set; }
        public List<DBitmap> Bitmaps { get; set; }
        public List<BioImage> Images { get; set; }
        private DTextureShader TextureShader { get; set; }
        public Bio.Graphics.DX.DTimer Timer { get; set; }

        // Construtor
        public DGraphics() { }

        // Methods
        public bool Initialize(Bio.Graphics.DX.DSystemConfiguration configuration, IntPtr windowsHandle, List<BioImage> images, float zoom)
        {
            try
            {
                // Create the Direct3D object.
                D3D = new DDX11();
                Images = images;
                // Initialize the Direct3D object.
                if (!D3D.Initialize(configuration, windowsHandle))
                    return false;

                // Create the Timer
                Timer = new Bio.Graphics.DX.DTimer();

                // Initialize the Timer
                if (!Timer.Initialize())
                    return false;

                // Create the camera object
                Camera = new DCamera();

                // Set the initial position of the camera.  moved closer inTutorial 7
                Camera.SetPosition((float)images[0].Volume.Location.X, (float)images[0].Volume.Location.Y, -zoom);

                // Create the texture shader object.
                TextureShader = new DTextureShader();

                // Initialize the texture shader object.
                if (!TextureShader.Initialize(D3D.Device, windowsHandle))
                {
                    MessageBox.Show("Could not initialize the texture shader object.");
                    return false;
                }

                Bitmaps = new List<DBitmap>();
                for (int i = 0; i < images.Count; i++)
                {
                    DBitmap bitmap = new DBitmap();
                    // Initialize the bitmap object.
                    if (!bitmap.Initialize(D3D.Device, configuration.Width, configuration.Height, images[i].SelectedBuffer))
                        return false;
                    // Create the bitmap object.
                    Bitmaps.Add(bitmap);
                }
                
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not initialize Direct3D\nError is '" + ex.Message + "'");
                return false;
            }
        }
        public void ShutDown()
        {
            Timer = null;
            Camera = null;

            // Release the color shader object.
            TextureShader?.ShutDown();
            TextureShader = null;
            // Release the model object.
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                Bitmaps[i]?.Shutdown();
                Bitmaps[i] = null;
            }
            // Release the Direct3D object.
            D3D?.ShutDown();
            D3D = null;
        }
        public bool Frame(float x, float y,int ScreenWidth, int ScreenHeight, Matrix world)
        {
            // Render the graphics scene.
            return Render(x,y, ScreenWidth, ScreenHeight, world);
        }
        private bool Render(float x, float y, int ScreenWidth, int ScreenHeight, Matrix worldMatrix)
        {
            // Clear the buffer to begin the scene.
            D3D.BeginScene(1f, 1f, 1f, 1f);

            
            // Generate the view matrix based on the camera position.
            Camera.Render();
            // Get the world, view, and projection matrices from camera and d3d objects.
            Matrix viewMatrix = Camera.ViewMatrix;
            //Matrix worldMatrix = D3D.WorldMatrix;
            Matrix projectionMatrix = D3D.ProjectionMatrix;
            Matrix orthoMatrix = D3D.OrthoMatrix;

            // Turn off the Z buffer to begin all 2D rendering.
            D3D.TurnZBufferOff();
            for (int i = 0; i < Bitmaps.Count; i++)
            {
                // Put the bitmap vertex and index buffers on the graphics pipeline to prepare them for drawing.
                if (!Bitmaps[i].Render(D3D.DeviceContext, (float)Images[i].Volume.Location.X, (float)Images[i].Volume.Location.Y, Bitmaps[i].BitmapWidth, Bitmaps[i].BitmapHeight, ScreenWidth, ScreenHeight))
                    return false;

                // Render the bitmap with the texture shader.
                if (!TextureShader.Render(D3D.DeviceContext, Bitmaps[i].IndexCount, worldMatrix, viewMatrix, orthoMatrix, Bitmaps[i].Texture.TextureResource))
                    return false;
            }
            

            // Turn the Z buffer back on now that all 2D rendering has completed.
            D3D.TurnZBufferOn();

            // Present the rendered scene to the screen.
            D3D.EndScene();

            return true;
        }
    }
}