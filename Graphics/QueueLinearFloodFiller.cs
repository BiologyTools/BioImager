namespace Bio
{
    /// <summary>
    /// Implements the QueueLinear flood fill algorithm using array-based pixel manipulation.
    /// </summary>
    public class QueueLinearFloodFiller : AbstractFloodFiller
    {

        //Queue of floodfill ranges. We use our own class to increase performance.
        //To use .NET Queue class, change this to:
        //<FloodFillRange> ranges = new Queue<FloodFillRange>();
        FloodFillRangeQueue ranges = new FloodFillRangeQueue();

        public QueueLinearFloodFiller(AbstractFloodFiller configSource) : base(configSource) { }

        /// <summary>
        /// Fills the specified point on the bitmap with the currently selected fill color.
        /// </summary>
        /// <param name="pt">The starting point for the fill.</param>
        public override void FloodFill(System.Drawing.Point pt)
        {
            watch.Reset();
            watch.Start();

            //***Prepare for fill.
            PrepareForFloodFill(pt);

            ranges = new FloodFillRangeQueue(((bitmapWidth + bitmapHeight) / 2) * 5);//new Queue<FloodFillRange>();

            //***Get starting color.
            int x = pt.X; int y = pt.Y;
            int idx = CoordsToByteIndex(ref x, ref y);
            startColor = bitmap.GetPixel(pt.X, pt.Y);

            bool[] pixelsChecked = this.pixelsChecked;

            //***Do first call to floodfill.
            LinearFill(ref x, ref y);

            //***Call floodfill routine while floodfill ranges still exist on the queue
            while (ranges.Count > 0)
            {
                //**Get Next Range Off the Queue
                FloodFillRange range = ranges.Dequeue();

                //**Check Above and Below Each Pixel in the Floodfill Range
                int downPxIdx = (bitmapWidth * (range.Y + 1)) + range.StartX;//CoordsToPixelIndex(lFillLoc,y+1);
                int upPxIdx = (bitmapWidth * (range.Y - 1)) + range.StartX;//CoordsToPixelIndex(lFillLoc, y - 1);
                int upY = range.Y - 1;//so we can pass the y coord by ref
                int downY = range.Y + 1;
                int tempIdx;
                for (int i = range.StartX; i <= range.EndX; i++)
                {
                    //*Start Fill Upwards
                    //if we're not above the top of the bitmap and the pixel above this one is within the color tolerance
                    tempIdx = CoordsToByteIndex(ref i, ref upY);
                    if (range.Y > 0 && (!pixelsChecked[upPxIdx]) && CheckPixel(ref tempIdx))
                        LinearFill(ref i, ref upY);

                    //*Start Fill Downwards
                    //if we're not below the bottom of the bitmap and the pixel below this one is within the color tolerance
                    tempIdx = CoordsToByteIndex(ref i, ref downY);
                    if (range.Y < (bitmapHeight - 1) && (!pixelsChecked[downPxIdx]) && CheckPixel(ref tempIdx))
                        LinearFill(ref i, ref downY);
                    downPxIdx++;
                    upPxIdx++;
                }

            }

            watch.Stop();
        }

        /// <summary>
        /// Finds the furthermost left and right boundaries of the fill area
        /// on a given y coordinate, starting from a given x coordinate, filling as it goes.
        /// Adds the resulting horizontal range to the queue of floodfill ranges,
        /// to be processed in the main loop.
        /// </summary>
        /// <param name="x">The x coordinate to start from.</param>
        /// <param name="y">The y coordinate to check at.</param>
        void LinearFill(ref int x, ref int y)
        {

            //cache some bitmap and fill info in local variables for a little extra speed

            byte[] bitmapBits = this.bitmapBits;
            bool[] pixelsChecked = this.pixelsChecked;
            byte[] byteFillColor = this.byteFillColor.GetBytes(pixelFormat);
            int bitmapPixelFormatSize = this.bitmapPixelFormatSize;
            int bitmapWidth = this.bitmapWidth;
            byte[] bt = new byte[6];
            //***Find Left Edge of Color Area
            int lFillLoc = x; //the location to check/fill on the left
            int idx = CoordsToByteIndex(ref x, ref y); //the byte index of the current location
            int pxIdx = (bitmapWidth * y) + x;//CoordsToPixelIndex(x,y);
            while (true)
            {
                //**fill with the color
                //bitmap.SetColor(x, y, this.byteFillColor);
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    bitmapBits[idx] = byteFillColor[0];
                }
                else
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    bitmapBits[idx] = byteFillColor[0];
                    bitmapBits[idx + 1] = byteFillColor[1];
                    bitmapBits[idx + 2] = byteFillColor[2];
                }
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb || pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb)
                {
                    bitmapBits[idx + 1] = byteFillColor[0];
                    bitmapBits[idx + 2] = byteFillColor[1];
                    bitmapBits[idx + 3] = byteFillColor[2];
                }
                else if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                {
                    bitmapBits[idx] = byteFillColor[0];
                    bitmapBits[idx + 1] = byteFillColor[1];
                }
                else if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format48bppRgb)
                {
                    bitmapBits[idx] = byteFillColor[0];
                    bitmapBits[idx + 1] = byteFillColor[1];
                    bitmapBits[idx + 2] = byteFillColor[2];
                    bitmapBits[idx + 3] = byteFillColor[3];
                    bitmapBits[idx + 4] = byteFillColor[4];
                    bitmapBits[idx + 5] = byteFillColor[5];
                }
                //**indicate that this pixel has already been checked and filled
                pixelsChecked[pxIdx] = true;
                //**de-increment
                lFillLoc--;     //de-increment counter
                pxIdx--;        //de-increment pixel index
                idx -= bitmapPixelFormatSize;//de-increment byte index
                //**exit loop if we're at edge of bitmap or color area
                if (lFillLoc <= 0 || (pixelsChecked[pxIdx]) || !CheckPixel(ref idx))
                    break;

            }
            lFillLoc++;

            //***Find Right Edge of Color Area
            int rFillLoc = x; //the location to check/fill on the left
            idx = CoordsToByteIndex(ref x, ref y);
            pxIdx = (bitmapWidth * y) + x;
            while (true)
            {
                //**fill with the color
                //bitmap.SetColor(x, y, this.byteFillColor);
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
                {
                    bitmapBits[idx] = byteFillColor[0];
                }
                else
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
                {
                    bitmapBits[idx] = byteFillColor[2];
                    bitmapBits[idx + 1] = byteFillColor[1];
                    bitmapBits[idx + 2] = byteFillColor[0];
                }
                else
                if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb || pixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb)
                {
                    bitmapBits[idx + 1] = byteFillColor[0];
                    bitmapBits[idx + 2] = byteFillColor[1];
                    bitmapBits[idx + 3] = byteFillColor[2];
                }
                else if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
                {
                    bitmapBits[idx] = byteFillColor[0];
                    bitmapBits[idx + 1] = byteFillColor[1];
                }
                else if (pixelFormat == System.Drawing.Imaging.PixelFormat.Format48bppRgb)
                {
                    bitmapBits[idx] = byteFillColor[0];
                    bitmapBits[idx + 1] = byteFillColor[1];
                    bitmapBits[idx + 2] = byteFillColor[2];
                    bitmapBits[idx + 3] = byteFillColor[3];
                    bitmapBits[idx + 4] = byteFillColor[4];
                    bitmapBits[idx + 5] = byteFillColor[5];
                }
                //**indicate that this pixel has already been checked and filled
                pixelsChecked[pxIdx] = true;
                //**increment
                rFillLoc++;     //increment counter
                pxIdx++;        //increment pixel index
                idx += bitmapPixelFormatSize;//increment byte index
                //**exit loop if we're at edge of bitmap or color area
                if (rFillLoc >= bitmapWidth || pixelsChecked[pxIdx] || !CheckPixel(ref idx))
                    break;

            }
            rFillLoc--;

            //add range to queue
            FloodFillRange r = new FloodFillRange(lFillLoc, rFillLoc, y);
            ranges.Enqueue(ref r);
        }

        ///<summary>Sees if a pixel is within the color tolerance range.</summary>
        ///<param name="px">The byte index of the pixel to check, passed by reference to increase performance.</param>
        protected bool CheckPixel(ref int px)
        {
            //tried a 'for' loop but it adds an 8% overhead to the floodfill process
            /*bool ret = true;
            for (byte i = 0; i < 3; i++)
            {
                ret &= (bitmap.Bits[px] >= (startColor[i] - tolerance[i])) && bitmap.Bits[px] <= (startColor[i] + tolerance[i]);
                px++;
            }
            return ret;*/
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format8bppIndexed)
            {
                return (bitmapBits[px] >= (startColor.R - tolerance.R)) && bitmapBits[px] <= (startColor.R + tolerance.R);
            }
            else
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format24bppRgb)
            {
                return (bitmapBits[px] >= (startColor.R - tolerance.R)) && bitmapBits[px] <= (startColor.R + tolerance.R) &&
                    (bitmapBits[px + 1] >= (startColor.G - tolerance.G)) && bitmapBits[px + 1] <= (startColor.G + tolerance.G) &&
                    (bitmapBits[px + 2] >= (startColor.B - tolerance.B)) && bitmapBits[px + 2] <= (startColor.B + tolerance.B);
            }
            else
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppArgb || bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format32bppRgb)
            {
                return (bitmapBits[px + 3] >= (startColor.R - tolerance.R)) && bitmapBits[px + 3] <= (startColor.R + tolerance.R) &&
                    (bitmapBits[px + 2] >= (startColor.G - tolerance.G)) && bitmapBits[px + 2] <= (startColor.G + tolerance.G) &&
                    (bitmapBits[px + 1] >= (startColor.B - tolerance.B)) && bitmapBits[px + 1] <= (startColor.B + tolerance.B);
            }
            else
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format16bppGrayScale)
            {
                ushort r = BitConverter.ToUInt16(bitmapBits, px);
                return (r >= (startColor.R - tolerance.R)) && r <= (startColor.R + tolerance.R);
            }
            else
            if (bitmap.PixelFormat == System.Drawing.Imaging.PixelFormat.Format48bppRgb)
            {
                ushort r = BitConverter.ToUInt16(bitmapBits, px);
                ushort g = BitConverter.ToUInt16(bitmapBits, px + 2);
                ushort b = BitConverter.ToUInt16(bitmapBits, px + 4);
                return (r >= (startColor.R - tolerance.R)) && r <= (startColor.R + tolerance.R) &&
                    (g >= (startColor.G - tolerance.G)) && g <= (startColor.G + tolerance.G) &&
                    (b >= (startColor.B - tolerance.B)) && b <= (startColor.B + tolerance.B);
            }
            throw new NotSupportedException(bitmap.PixelFormat + " not supported");
        }

        ///<summary>Calculates and returns the byte index for the pixel (x,y).</summary>
        ///<param name="x">The x coordinate of the pixel whose byte index should be returned.</param>
        ///<param name="y">The y coordinate of the pixel whose byte index should be returned.</param>
        protected int CoordsToByteIndex(ref int x, ref int y)
        {
            return (bitmapStride * y) + (x * bitmapPixelFormatSize);
        }

        /// <summary>
        /// Returns the linear index for a pixel, given its x and y coordinates.
        /// </summary>
        /// <param name="x">The x coordinate of the pixel.</param>
        /// <param name="y">The y coordinate of the pixel.</param>
        /// <returns></returns>
        protected int CoordsToPixelIndex(int x, int y)
        {
            return (bitmapWidth * y) + x;
        }

    }

    /// <summary>
    /// Represents a linear range to be filled and branched from.
    /// </summary>
    public struct FloodFillRange
    {
        public int StartX;
        public int EndX;
        public int Y;

        public FloodFillRange(int startX, int endX, int y)
        {
            StartX = startX;
            EndX = endX;
            Y = y;
        }
    }
}
