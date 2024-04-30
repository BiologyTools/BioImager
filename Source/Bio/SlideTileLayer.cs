using BruTile;
using Mapsui;
using Mapsui.Fetcher;
using Mapsui.Layers;
using Mapsui.Providers;
using Mapsui.Rendering;
using Mapsui.Tiling.Fetcher;
using Mapsui.Tiling.Layers;
using Mapsui.Tiling.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BioImager
{
    /// <summary>
    /// Slide tile layer
    /// </summary>
    public class SlideTileLayer : TileLayer
    {
        private ISlideSource _slideSource;

        public SlideTileLayer(
            ISlideSource source = null,
            int minTiles = 200,
            int maxTiles = 300,
            IDataFetchStrategy dataFetchStrategy = null,
            IRenderFetchStrategy renderFetchStrategy = null,
            int minExtraTiles = -1,
            int maxExtraTiles = -1,
            Func<TileInfo, Task<IFeature>> fetchTileAsFeature = null)
            : base(source, minTiles, maxTiles, dataFetchStrategy, renderFetchStrategy, minExtraTiles, maxExtraTiles, (Func<BruTile.TileInfo, Task<IFeature>>)fetchTileAsFeature)
        {
            Name = "TileLayer";
            _slideSource = source;
        }

        public override IReadOnlyList<double> Resolutions
        {
            get
            {
                var resolution = new List<double>()
                    {
                       0.0625,0.125,0.25,0.5,1,2,4,8,16,32,64,128,256,512,1024,2048,4096
                    };
                var values = _slideSource.Schema.Resolutions.Values.Select(_ => _.UnitsPerPixel);
                return GetNearest(resolution, values);
            }
        }


        private IReadOnlyList<double> GetNearest(IEnumerable<double> rs, IEnumerable<double> r)
        {
            if (r.Count() > 1)
                return rs.ToList();
            var input = r.OrderBy(_ => _);
            var output = rs.OrderBy(_ => _);
            var min = input.FirstOrDefault();
            var max = input.LastOrDefault();
            var minIndex = -1;
            var maxIndex = -1;
            var lastItem = -1d;
            var index = 0;
            foreach (var item in output)
            {
                if (lastItem < min && item >= min)
                    minIndex = index - 1;
                if (lastItem <= max && item > max)
                {
                    maxIndex = index + 1;
                    break;
                }
                index++;
                lastItem = item;
            }
            return output.Skip(minIndex).Take(maxIndex - minIndex).ToList();
        }
    }
}

