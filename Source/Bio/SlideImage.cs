using OpenSlideGTK;
using OpenSlideGTK.Interop;
using org.checkerframework.common.returnsreceiver.qual;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BioImager
{
    /// <summary>
    /// openslide wrapper
    /// </summary>
    public partial class SlideImage : IDisposable
    {
        public BioImage BioImage { get; set; }


        /// <summary>
        /// Quickly determine whether a whole slide image is recognized.
        /// </summary>
        /// <remarks>
        /// If OpenSlide recognizes the file referenced by <paramref name="filename"/>, 
        /// return a string identifying the slide format vendor.This is equivalent to the
        /// value of the <see cref="NativeMethods.VENDOR"/> property. Calling
        /// <see cref="Open(string)"/> on this file will return a valid 
        /// OpenSlide object or an OpenSlide object in error state.
        ///
        /// Otherwise, return <see langword="null"/>.Calling <see cref="
        /// Open(string)"/> on this file will also
        /// return <see langword="null"/>.</remarks>
        /// <param name="filename">The filename to check. On Windows, this must be in UTF-8.</param>
        /// <returns>An identification of the format vendor for this file, or NULL.</returns>
        public static string DetectVendor(string filename)
        {
            return filename;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="isOwner">close handle when disposed</param>
        /// <exception cref="OpenSlideException"/>
        public SlideImage()
        {
            
        }

        /// <summary>
        /// Add .dll directory to PATH
        /// </summary>
        /// <param name="path"></param>
        /// <exception cref="OpenSlideException"/>
        public static void Initialize(string path = null)
        {
            
        }
        static SlideImage()
        {
            Initialize();
        }

        /// <summary>
        /// Open.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="OpenSlideException"/>
        public static SlideImage Open(BioImage b)
        {
            SlideImage im = new SlideImage();
            im.BioImage = b;
            return im;
        }

        /// <summary>
        /// Get the number of levels in the whole slide image.
        /// </summary>
        /// <return>The number of levels, or -1 if an error occurred.</return> 
        /// <exception cref="OpenSlideException"/>
        public int LevelCount
        {
            get
            {
                if (BioImage.MacroResolution.HasValue)
                {
                    return BioImage.Resolutions.Count - 2;
                }
                else
                    return BioImage.Resolutions.Count;
            }
        }

        private ImageDimension? _dimensionsRef;
        private readonly object _dimensionsSynclock = new object();

        /// <summary>
        /// Get the dimensions of level 0 (the largest level). Exactly
        /// equivalent to calling GetLevelDimensions(0).
        /// </summary>
        /// <exception cref="OpenSlideException"/>
        public ImageDimension Dimensions
        {
            get
            {
                if (_dimensionsRef == null)
                {
                    lock (_dimensionsSynclock)
                    {
                        if (_dimensionsRef == null)
                            _dimensionsRef = GetLevelDimension(0);
                    }
                }
                return _dimensionsRef.Value;
            }
        }
        /// <summary>
        /// Get the dimensions of a level.
        /// </summary>
        /// <param name="level">The desired level.</param>
        /// <exception cref="OpenSlideException"/>
        public ImageDimension GetLevelDimension(int level)
        {
            return new ImageDimension(BioImage.Resolutions[level].SizeX, BioImage.Resolutions[level].SizeY);
        }

        /// <summary>
        /// Get all level dimensions.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="OpenSlideException"/>
        public IEnumerable<ImageDimension> GetLevelDimensions()
        {
            var count = LevelCount;
            for (int i = 0; i < count; i++)
            {
                yield return GetLevelDimension(i);
            }
        }
        /// <summary>
        /// Calculates the base downsampling factor between two levels of a slide.
        /// </summary>
        /// <param name="originalDimension">The dimension (width or height) of the original level.</param>
        /// <param name="nextLevelDimension">The dimension (width or height) of the next level.</param>
        /// <returns>The base downsampling factor.</returns>
        public static double CalculateBaseFactor(int originalResolution, int lastLevelResolution, int totalLevels)
        {
            if (totalLevels <= 1)
            {
                throw new ArgumentException("Total levels must be greater than 1 to calculate a base factor.");
            }
            if (lastLevelResolution <= 0 || originalResolution <= 0)
            {
                throw new ArgumentException("Resolutions must be greater than 0.");
            }

            // Calculate the base downsampling factor
            double baseFactor = Math.Pow((double)originalResolution / lastLevelResolution, 1.0 / (totalLevels - 1));
            return baseFactor;
        }

        /// <summary>
        /// Calculates the downsample factors for each level of a slide.
        /// </summary>
        /// <param name="baseDownsampleFactor">The downsample factor between each level.</param>
        /// <param name="totalLevels">Total number of levels in the slide.</param>
        /// <returns>A list of downsample factors for each level.</returns>
        public static List<double> GetLevelDownsamples(double baseDownsampleFactor, int totalLevels)
        {
            var levelDownsamples = new List<double>();

            for (int level = 0; level < totalLevels; level++)
            {
                // Calculate the downsample factor for the current level.
                // Math.Pow is used to raise the baseDownsampleFactor to the power of the level.
                double downsampleFactorAtLevel = Math.Pow(baseDownsampleFactor, level);
                levelDownsamples.Add(downsampleFactorAtLevel);
            }

            return levelDownsamples;
        }
        

        /*
        /// <summary>
        /// Get the best level to use for displaying the given downsample.
        /// </summary>
        /// <param name="downsample">The downsample factor.</param> 
        /// <return>The level identifier, or -1 if an error occurred.</return> 
        /// <exception cref="OpenSlideException"/>
        public int GetBestLevelForDownsample(double downsample)
        {
            if (NativeMethods.isWindows)
            {
                var result = NativeMethods.Windows.GetBestLevelForDownsample(Handle, downsample);
                return result != -1 ? result : CheckIfThrow(result);
            } else if (NativeMethods.isLinux)
            {
                var result = NativeMethods.Linux.GetBestLevelForDownsample(Handle, downsample);
                return result != -1 ? result : CheckIfThrow(result);
            }
            else
            {
                var result = NativeMethods.OSX.GetBestLevelForDownsample(Handle, downsample);
                return result != -1 ? result : CheckIfThrow(result);
            }
        }
        */
        /// <summary>
        /// Copy pre-multiplied BGRA data from a whole slide image.
        /// </summary>
        /// <param name="level">The desired level.</param>
        /// <param name="x">The top left x-coordinate, in the level 0 reference frame.</param>
        /// <param name="y">The top left y-coordinate, in the level 0 reference frame.</param>
        /// <param name="width">The width of the region. Must be non-negative.</param>
        /// <param name="height">The height of the region. Must be non-negative.</param>
        /// <returns>The pixel data of this region.</returns>
        /// <exception cref="ArgumentOutOfRangeException"/>
        /// <exception cref="OpenSlideException"/>
        public unsafe byte[] ReadRegion(int level, long x, long y, long width, long height)
        {
            return BioImage.GetTile(BioImage, App.viewer.GetCoordinate(), level, (int)x, (int)y, (int)width, (int)height).RGBBytes;
        }

        /// <summary>
        /// Copy pre-multiplied BGRA data from a whole slide image.
        /// </summary>
        /// <param name="level">The desired level.</param>
        /// <param name="x">The top left x-coordinate, in the level 0 reference frame.</param>
        /// <param name="y">The top left y-coordinate, in the level 0 reference frame.</param>
        /// <param name="width">The width of the region. Must be non-negative.</param>
        /// <param name="height">The height of the region. Must be non-negative.</param>
        /// <param name="data">The BGRA pixel data of this region.</param>
        /// <returns></returns>
        public unsafe bool TryReadRegion(int level, long x, long y, long width, long height, out byte[] data)
        {
            try
            {
                data = BioImage.GetTile(BioImage, App.viewer.GetCoordinate(), level, (int)x, (int)y, (int)width, (int)height).RGBBytes;
                if (data == null)
                    return false;
                else
                    return true;
            }
            catch (Exception e)
            {
                data = null;
                return false;
            }
            
        }

        ///<summary>
        ///Close an OpenSlide object.
        ///</summary>
        ///<remarks>
        ///No other threads may be using the object.
        ///After this call returns, the object cannot be used anymore.
        ///</remarks>
        public void Close()
        {
           
        }

        #region IDisposable

        private bool disposedValue;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                Close();
                disposedValue = true;
            }
        }

        /// <summary>
        /// </summary>
        ~SlideImage()
        {
            Dispose(disposing: false);
        }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<byte[]> ReadRegionAsync(int level, long curLevelOffsetXPixel, long curLevelOffsetYPixel, int curTileWidth, int curTileHeight)
        {
            try
            {
                byte[] bts;
                TryReadRegion(level, curLevelOffsetXPixel, curLevelOffsetYPixel, curTileWidth, curTileHeight,out bts);
                return bts;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion
    }
}
