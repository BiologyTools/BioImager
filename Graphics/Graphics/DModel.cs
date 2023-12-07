using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using AForge;
namespace BioImager
{
    public class DModel
    {
        // Properties
        private SharpDX.Direct3D11.Buffer VertexBuffer { get; set; }
        private SharpDX.Direct3D11.Buffer IndexBuffer { get; set; }
        private int VertexCount { get; set; }
        public int IndexCount { get; set; }

        // Constructor
        public DModel() { }

        // Methods.
        public bool Initialize(Device device, BioImage im)
        {
            // Initialize the vertex and index buffer that hold the geometry for the triangle.
            return InitializeBuffer(device, im);
        }
        public void ShutDown()
        {
            // Release the vertex and index buffers.
            ShutDownBuffers();
        }
        public void Render(DeviceContext deviceContext)
        {
            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext);
        }
        private bool InitializeBuffer(Device device, BioImage im)
        {
            // Set number of vertices in the vertex array.
            VertexCount = im.SizeX * im.SizeY * im.SizeZ;
            // Set number of vertices in the index array.
            IndexCount = im.SizeX * im.SizeY * im.SizeZ;
            // Create the vertex array and load it with data.
            var vertices = new DColorShader.DVertex[VertexCount];
            ColorS col;
            for (int z = 0; z < im.SizeZ; z++)
            {
                for (int y = im.SizeY - 1; y >= 0; y--)
                {
                    for (int x = im.SizeX - 1; x >= 0; x--)
                    {
                        col = im.Buffers[im.Coords[z, 0, 0]].GetPixel(x, y);
                        int ind = (im.SizeX * y + x) * (z + 1);
                        Vector4 vec = new Vector4(col.Rf,col.Gf,col.Bf,0.5f);
                        vertices[ind] = new DColorShader.DVertex()
                        {
                            position = new Vector3((float)x / (float)im.SizeX, (float)y / (float)im.SizeY, (z / (float)im.SizeZ) * (float)im.PhysicalSizeZ),
                            color = vec
                        };
                    }
                };
            }
            // Create Indicies for the IndexBuffer.
            int[] indicies = new int[IndexCount];
            for (int i = 0; i < indicies.Length; i++)
            {
                indicies[i] = i;
            }

            // Create the vertex buffer.
            VertexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.VertexBuffer, vertices);

            // Create the index buffer.
            IndexBuffer = SharpDX.Direct3D11.Buffer.Create(device, BindFlags.IndexBuffer, indicies);

            // Delete arrays now that they are in their respective vertex and index buffers.
            vertices = null;
            indicies = null;
            GC.Collect();
            return true;
        }
        private void ShutDownBuffers()
        {
            // Release the index buffer.
            IndexBuffer?.Dispose();
            IndexBuffer = null;
            // Release the vertex buffer.
            VertexBuffer?.Dispose();
            VertexBuffer = null;
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, Utilities.SizeOf<DColorShader.DVertex>(), 0));

            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);

            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.PointList;
        }
    }
}