using Bio.Graphics.DX;
using SharpDX;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;

namespace Bio.Graphics.DX.Graphics
{
    public class DBitmap                    // 210 lines
    {
        // Properties.
        public Buffer VertexBuffer { get; set; }
        public Buffer IndexBuffer { get; set; }
        public int VertexCount { get; set; }
        public int IndexCount { get; private set; }
        public DTexture Texture { get; set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public int BitmapWidth { get; private set; }
        public int BitmapHeight { get; private set; }

        // Constructor
        public DBitmap() { }

        // Methods
        public bool Initialize(Device device, int screeenWidth, int screenHeight, BufferInfo bf)
        {
            // Store the screen size.
            ScreenWidth = screeenWidth;
            ScreenHeight = screenHeight;

            // Store the size in pixels that this bitmap should be rendered at.
            BitmapWidth = bf.SizeX;
            BitmapHeight = bf.SizeY;

            // Initialize the vertex and index buffer.
            if (!InitializeBuffers(device))
                return false;

            // Load the texture for this model.
            if (!LoadTexture(device, bf))
                return false;

            return true;
        }
        public void Shutdown()
        {
            // Release the model texture.
            ReleaseTexture();

            // Release the vertex and index buffers.
            ShutdownBuffers();
        }
        private void ShutdownBuffers()
        {
            // Return the index buffer.
            IndexBuffer?.Dispose();
            IndexBuffer = null;
            // Release the vertex buffer.
            VertexBuffer?.Dispose();
            VertexBuffer = null;
        }
        public bool Render(DeviceContext deviceContext, float positionX, float positionY, float width, float height, int ScreenWidth, int ScreenHeight)
        {
            // Re-build the dynamic vertex buffer for rendering to possibly a different location on the screen.
            if (!UpdateBuffers(deviceContext, positionX, positionY, width, height, ScreenWidth, ScreenHeight))
                return false;

            // Put the vertex and index buffers on the graphics pipeline to prepare for drawings.
            RenderBuffers(deviceContext);

            return true;
        }
        private bool InitializeBuffers(Device device)
        {
            try
            {
                // Set the number of the vertices and indices in the vertex and index array, accordingly.
                VertexCount = 6;
                IndexCount = 6;

                // Create the vertex array.
                var vertices = new DTextureShader.DVertex[VertexCount];
                // Create the index array.
                var indices = new int[IndexCount];

                // Load the index array with data.
                for (var i = 0; i < IndexCount; i++)
                    indices[i] = i;

                // Set up the description of the static vertex buffer.
                var vertexBuffer = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DTextureShader.DVertex>() * VertexCount,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the vertex buffer.
                VertexBuffer = Buffer.Create(device, vertices, vertexBuffer);

                // Create the index buffer.
                IndexBuffer = Buffer.Create(device, BindFlags.IndexBuffer, indices);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool UpdateBuffers(DeviceContext deviceContext, float positionX, float positionY, float width, float height, int ScreenWidth, int ScreenHeight)
        {
            this.ScreenWidth = ScreenWidth;
            this.ScreenHeight = ScreenHeight;
            
            // Calculate the screen coordinates of the left side of the bitmap.
            var left = -(ScreenWidth / 2) + (float)positionX;
            // Calculate the screen coordinates of the right side of the bitmap.
            var right = (left + width);
            // Calculate the screen coordinates of the top of the bitmap.
            var top = (ScreenHeight / 2) - (float)positionY;
            // Calculate the screen coordinates of the bottom of the bitmap.
            var bottom = (top - height);
            
            /*
            // Calculate the screen coordinates of the left side of the bitmap.
            var left = (float)((ScreenWidth / 2) * -1) + (float)positionX;

            // Calculate the screen coordinates of the right side of the bitmap.
            var right = left + (float)width;

            // Calculate the screen coordinates of the top of the bitmap.
            var top = (float)(ScreenHeight / 2) - (float)positionY;

            // Calculate the screen coordinates of the bottom of the bitmap.
            var bottom = top - (float)height;
            */
            // Create and load the vertex array.
            var vertices = new[] 
			{
				new DTextureShader.DVertex()
				{
					position = new Vector3(left, top, 0),
					texture = new Vector2(0, 0)
				},
				new DTextureShader.DVertex()
				{
					position = new Vector3(right, bottom, 0),
					texture = new Vector2(1, 1)
				},
				new DTextureShader.DVertex()
				{
					position = new Vector3(left, bottom, 0),
					texture = new Vector2(0, 1)
				},
				new DTextureShader.DVertex()
				{
					position = new Vector3(left, top, 0),
					texture = new Vector2(0, 0)
				},
				new DTextureShader.DVertex()
				{
					position = new Vector3(right, top, 0),
					texture = new Vector2(1, 0)
				},
				new DTextureShader.DVertex()
				{
					position = new Vector3(right, bottom, 0),
					texture = new Vector2(1, 1)
				}
			};

            DataStream mappedResource;

            #region Vertex Buffer
            // Lock the vertex buffer so it can be written to.
            deviceContext.MapSubresource(VertexBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

            // Copy the data into the vertex buffer.
            mappedResource.WriteRange<DTextureShader.DVertex>(vertices);

            // Unlock the vertex buffer.
            deviceContext.UnmapSubresource(VertexBuffer, 0);
            #endregion

            return true;
        }
        private void RenderBuffers(DeviceContext deviceContext)
        {
            // Set vertex buffer stride and offset.
            var stride = Utilities.SizeOf<DTextureShader.DVertex>();
            var offset = 0;

            // Set the vertex buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(VertexBuffer, stride, offset));
            
            // Set the index buffer to active in the input assembler so it can be rendered.
            deviceContext.InputAssembler.SetIndexBuffer(IndexBuffer, SharpDX.DXGI.Format.R32_UInt, 0);
            
            // Set the type of the primitive that should be rendered from this vertex buffer, in this case triangles.
            deviceContext.InputAssembler.PrimitiveTopology = PrimitiveTopology.TriangleList;
        }
        private bool LoadTexture(Device device, BufferInfo bf)
        {
            // Create the texture object.
            if(Texture == null)
            Texture = new DTexture();

            // Initialize the texture object.
            Texture.Initialize(device, bf);

            return true;
        }
        private void ReleaseTexture()
        {
            // Release the texture object.
            Texture?.ShutDown();
            Texture = null;
        }
    }
}