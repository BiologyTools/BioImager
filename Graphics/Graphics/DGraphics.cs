using AForge;
using BioLib;
namespace BioImager
{
    public class DGraphics                  // 113 lines
    {
        // Properties 
        public DDX11 D3D { get; set; }
        public DCamera Camera { get; set; }
        public DModel Model { get; set; }
        private DColorShader ColorShader { get; set; }
        public DTimer Timer { get; set; }
        public int BitsPerPixel { get; set; }
        // Constructor
        public DGraphics() { }

        // Methods
        public bool Initialize(DSystemConfiguration configuration, IntPtr windowHandle, BioImage im)
        {
            try
            {
                // Create the Direct3D object.
                D3D = new DDX11();

                // I nitialize the Direct3D object.
                if (!D3D.Initialize(configuration, windowHandle))
                    return false;

                // Create the camera object
                Camera = new DCamera();

                // Set the initial position of the camera.
                Camera.SetPosition(0.1f, -0.1f, -1);

                // Create the model object.
                Model = new DModel();

                // Initialize the model object.
                if (!Model.Initialize(D3D.Device, im))
                    return false;

                // Create the color shader object.
                ColorShader = new DColorShader();

                // Initialize the color shader object.
                if (!ColorShader.Initialize(D3D.Device, windowHandle, im.bitsPerPixel))
                    return false;

                // Create the Timer
                Timer = new DTimer();

                // Initialize the Timer
                if (!Timer.Initialize())
                    return false;
                BitsPerPixel = im.bitsPerPixel;
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
            // Release the camera object.
            Camera = null;
            Timer = null;

            // Release the color shader object.
            ColorShader?.ShutDown();
            ColorShader = null;
            // Release the model object.
            Model?.ShutDown();
            Model = null;
            // Release the Direct3D object.
            D3D?.ShutDown();
            D3D = null;
        }
        public bool Frame(IntRange r, IntRange g, IntRange b, float interval, float alpha)
        {
            // Render the graphics scene.
            return Render(r, g, b, interval, alpha);
        }
        private bool Render(IntRange r, IntRange g, IntRange b, float interval, float alpha)
        {
            // Clear the buffer to begin the scene.
            D3D.BeginScene(0.0f, 0.0f, 0.0f, 1f);

            // Generate the view matrix based on the camera position.
            Camera.Render();

            // Get the world, view, and projection matrices from camera and d3d objects.
            var viewMatrix = Camera.ViewMatrix;
            var worldMatrix = D3D.WorldMatrix;
            var projectionMatrix = D3D.ProjectionMatrix;

            // Put the model vertex and index buffers on the graphics pipeline to prepare them for drawing.
            Model.Render(D3D.DeviceContext);

            // Render the model using the color shader.
            if (!ColorShader.Render(D3D.DeviceContext, Model.IndexCount, worldMatrix, viewMatrix, projectionMatrix, r, g, b, interval, alpha))
                return false;

            // Present the rendered scene to the screen.
            D3D.EndScene();

            return true;
        }
    }
}