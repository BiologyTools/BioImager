using AForge;
using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Bio.Graphics
{
    public class DColorShader                   // 199 lines
    {
        // Structures.
        [StructLayout(LayoutKind.Sequential)]
        internal struct DVertex
        {
            public static int AppendAlignedElement = 12;
            public Vector3 position;
            public Vector4 color;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct DMinMaxBuffer
        {
            public Vector4 rMinMax;
            public Vector4 gMinMax;
            public Vector4 bMinMax;
        }

        // Properties.
        public VertexShader VertexShader { get; set; }
        public PixelShader PixelShader { get; set; }
        public GeometryShader GeometryShader { get; set; }
        public InputLayout Layout { get; set; }
        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }
        public SharpDX.Direct3D11.Buffer ConstantMinMaxBuffer { get; set; }
        public int BitsPerPixel { get; set; }

        // Constructor
        public DColorShader() { }

        // Methods.
        public bool Initialize(Device device, IntPtr windowsHandle, int bitsPerPixel)
        {
            // Initialize the vertex and pixel shaders.
            return InitializeShader(device, windowsHandle,"Color-vs.hlsl", "Color-ps.hlsl", "Color-gs.hlsl", bitsPerPixel);
        }
        private bool InitializeShader(Device device, IntPtr windowsHandle, string vsFileName, string psFileName, string geoFileName, int bitsPerPixel)
        {
            // Setup full pathes
            vsFileName = DSystemConfiguration.ShaderFilePath + vsFileName;
            psFileName = DSystemConfiguration.ShaderFilePath + psFileName;
            geoFileName = DSystemConfiguration.ShaderFilePath + geoFileName;
            BitsPerPixel = bitsPerPixel;
            // Compile the vertex shader code.
            ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "ColorVertexShader", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            // Compile the pixel shader code.
            ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "ColorPixelShader", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            // Compile the geometry shader code.
            ShaderBytecode geoShaderByteCode = ShaderBytecode.CompileFromFile(geoFileName, "ColorGeometryShader", "gs_4_0", ShaderFlags.None, EffectFlags.None);

            // Create the vertex shader from the buffer.
            VertexShader = new VertexShader(device, vertexShaderByteCode);
            // Create the pixel shader from the buffer.
            PixelShader = new PixelShader(device, pixelShaderByteCode);
            // Create the geo shader from the buffer.
            GeometryShader = new GeometryShader(device, geoShaderByteCode);

            // Now setup the layout of the data that goes into the shader.
            // This setup needs to match the VertexType structure in the Model and in the shader.
            InputElement[] inputElements = new InputElement[] 
            {
                new InputElement()
                {
                    SemanticName = "POSITION",
                    SemanticIndex = 0,
                    Format = SharpDX.DXGI.Format.R32G32B32_Float,
                    Slot = 0,
                    AlignedByteOffset = 0,
                    Classification = InputClassification.PerVertexData,
                    InstanceDataStepRate = 0
                },
                new InputElement() 
                {
                    SemanticName = "COLOR",
                    SemanticIndex = 0,
                    Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                    Slot = 0,
                    AlignedByteOffset = InputElement.AppendAligned,
                    Classification = InputClassification.PerVertexData,
                    InstanceDataStepRate = 0
                }
            };

            // Create the vertex input the layout.
            Layout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

            // Release the vertex and pixel shader buffers, since they are no longer needed.
            vertexShaderByteCode.Dispose();
            pixelShaderByteCode.Dispose();
            geoShaderByteCode.Dispose();

            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            BufferDescription matrixBufDesc = new BufferDescription() 
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
            ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufDesc);

            // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
            BufferDescription minmaxBufDesc = new BufferDescription()
            {
                Usage = ResourceUsage.Dynamic,
                SizeInBytes = Utilities.SizeOf<DMinMaxBuffer>(),
                BindFlags = BindFlags.ConstantBuffer,
                CpuAccessFlags = CpuAccessFlags.Write,
                OptionFlags = ResourceOptionFlags.None,
                StructureByteStride = 0
            };

            // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
            ConstantMinMaxBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufDesc);

            return true;
        }
        public void ShutDown()
        {
            // Shutdown the vertex and pixel shaders as well as the related objects.
            ShuddownShader();
        }
        private void ShuddownShader()
        {
            // Release the matrix constant buffer.
            ConstantMatrixBuffer?.Dispose();
            ConstantMatrixBuffer = null;
            // Release the layout.
            Layout?.Dispose();
            Layout = null;
            // Release the pixel shader.
            PixelShader?.Dispose();
            PixelShader = null;
            // Release the vertex shader.
            VertexShader?.Dispose();
            VertexShader = null;
        }
        public bool Render(DeviceContext deviceContext, int indexCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, IntRange r, IntRange g, IntRange b)
        {
            // Set the shader parameters that it will use for rendering.
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, r, g, b))
                return false;

            // Now render the prepared buffers with the shader.
            RenderShader(deviceContext, indexCount);

            return true;
        }
        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, IntRange r, IntRange g, IntRange b)
        {
            try
            {
                // Transpose the matrices to prepare them for shader.
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();
                
                // Lock the constant buffer so it can be written to.
                DataStream mappedResource;
                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource);

                // Copy the matrices into the constant buffer.
                DMatrixBuffer matrixBuffer = new DMatrixBuffer()
                {
                    world = worldMatrix,
                    view = viewMatrix,
                    projection = projectionMatrix
                };
                mappedResource.Write(matrixBuffer);

                // Unlock the constant buffer.
                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);

                // Set the position of the constant buffer in the vertex shader.
                int bufferSlotNuber = 0;

                // Finally set the constant buffer in the vertex shader with the updated values.
                deviceContext.VertexShader.SetConstantBuffer(bufferSlotNuber, ConstantMatrixBuffer);

                // Lock the constant buffer so it can be written to.
                DataStream mappedResource2;
                deviceContext.MapSubresource(ConstantMinMaxBuffer, MapMode.WriteDiscard, MapFlags.None, out mappedResource2);

                // Copy the matrices into the constant buffer.
                DMinMaxBuffer minmaxBuffer;
                if (BitsPerPixel > 8)
                {
                    float f = ushort.MaxValue;
                    minmaxBuffer = new DMinMaxBuffer()
                    {
                        rMinMax = new Vector4((float)r.Min / f, (float)r.Max / f, 0, 0),
                        gMinMax = new Vector4((float)g.Min / f, (float)g.Max / f, 0, 0),
                        bMinMax = new Vector4((float)b.Min / f, (float)b.Max / f, 0, 0)
                    };
                }
                else
                {
                    float f = byte.MaxValue;
                    minmaxBuffer = new DMinMaxBuffer()
                    {
                        rMinMax = new Vector4((float)r.Min / f, (float)r.Max / f, 0, 0),
                        gMinMax = new Vector4((float)g.Min / f, (float)g.Max / f, 0, 0),
                        bMinMax = new Vector4((float)b.Min / f, (float)b.Max / f, 0, 0)
                    };
                }
                mappedResource2.Write(matrixBuffer);

                // Unlock the constant buffer.
                deviceContext.UnmapSubresource(ConstantMinMaxBuffer, 1);

                // Set the position of the constant buffer in the vertex shader.
                int bufferSlotNuber2 = 0;

                // Finally set the constant buffer in the vertex shader with the updated values.
                deviceContext.GeometryShader.SetConstantBuffer(bufferSlotNuber, ConstantMinMaxBuffer);

                return true;
            }
            catch
            {
                return false;
            }
        }
        private void RenderShader(DeviceContext deviceContext, int indexCount)
        {
            // Set the vertex input layout.
            deviceContext.InputAssembler.InputLayout = Layout;

            // Set the vertex and pixel shaders that will be used to render this triangle.
            deviceContext.VertexShader.Set(VertexShader);
            deviceContext.PixelShader.Set(PixelShader);
            deviceContext.GeometryShader.Set(GeometryShader);

            // Render the triangle.
            deviceContext.DrawIndexed(indexCount, 0, 0);
        }
    }
}