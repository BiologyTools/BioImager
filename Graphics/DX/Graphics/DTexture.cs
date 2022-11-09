using SharpDX.Direct3D11;
using SharpDX.WIC;
using System.Runtime.InteropServices;
using System.Drawing;
using System;

namespace Bio.Graphics.DX.Graphics
{
    public class DTexture                   // 31 lines
    {
        // Properties
        public ShaderResourceView TextureResource { get; private set; }
        public ShaderResourceViewDescription ViewDescription { get; private set; }
        // Methods.
        public bool Initialize(Device device, BufferInfo bf)
        {
            try
            {
                if (TextureResource != null)
                    ShutDown();
                using (var texture = CreateTextureFromBuffer(device,bf))
                {
                    ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription()
                    {
                        Format = texture.Description.Format,
                        Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    };
                    srvDesc.Texture2D.MostDetailedMip = 0;
                    srvDesc.Texture2D.MipLevels = -1;

                    TextureResource = new ShaderResourceView(device, texture, srvDesc);
                    device.ImmediateContext.GenerateMips(TextureResource);
                }
                // TextureResource = ShaderResourceView.FromFile(device, fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            TextureResource?.Dispose();
            TextureResource = null;
            GC.Collect();
        }
        public BitmapSource LoadBitmap(ImagingFactory factory, string filename)
        {
            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(
                factory,
                filename,
                SharpDX.WIC.DecodeOptions.CacheOnDemand
                );

            var result = new SharpDX.WIC.FormatConverter(factory);

            result.Initialize(
                bitmapDecoder.GetFrame(0),
                SharpDX.WIC.PixelFormat.Format32bppPRGBA,
                SharpDX.WIC.BitmapDitherType.None,
                null,
                0.0,
                SharpDX.WIC.BitmapPaletteType.Custom);

            return result;
        }
        public Texture2D CreateTextureFromBuffer(Device device, BufferInfo buf)
        {
            int stride = buf.SizeX * 4;
            // Allocate DataStream to receive the WIC image pixels
            SharpDX.DataPointer dp = new SharpDX.DataPointer(buf.RGBData, (int)stride * buf.SizeY);

            using (var buffer = new SharpDX.DataStream(dp))
            {
                return new Texture2D(device, new SharpDX.Direct3D11.Texture2DDescription()
                {
                    Width = buf.SizeX,
                    Height = buf.SizeY,
                    ArraySize = 1,
                    BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                    CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps, // ResourceOptionFlags.GenerateMipMap
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
            }
        }
    }
}