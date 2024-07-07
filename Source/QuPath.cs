using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using AForge;
using BioLib;
namespace BioImager
{
    public class QuPath
    {
        public class GeoJsonFeatureCollection
        {
            public string type { get; set; }
            public List<GeoJsonFeature> features { get; set; }
        }

        public class GeoJsonFeature
        {
            public string type { get; set; }
            public GeoJsonGeometry geometry { get; set; }
            public IDictionary<string, object> properties { get; set; }
        }

        public class GeoJsonGeometry
        {
            public class GeoJsonPlane
            {
                public int c { get; set; }
                public int z { get; set; }
                public int t { get; set; }
            }
            public string type { get; set; }
            public GeoJsonPlane plane {  get; set; }
            public object coordinates { get; set; }
            
            public ZCT GetZCT()
            {
                if (plane.c < 0)
                    return new ZCT(plane.z,0,plane.t);
                else
                    return new ZCT(plane.z,plane.c,plane.t);
            }
        }

        public class GeoJsonPoint
        {
            public class GeoJsonPlane
            {
                public int c { get; set; }
                public int z { get; set; }
                public int t { get; set; }
            }
            public string type { get; set; }
            public double[] coordinates { get; set; }
            public GeoJsonPlane plane { get; set; }
            public static GeoJsonPoint FromROI(ROI roi, BioImage b)
            {
                GeoJsonPoint g = new GeoJsonPoint();
                PointD p = b.ToImageSpace(roi.PointsD[0]);
                g.coordinates = new double[2] { p.X, p.Y };
                g.type = "Point";
                g.plane = new GeoJsonPlane();
                g.plane.z = roi.coord.Z;
                g.plane.c = roi.coord.C;
                g.plane.t = roi.coord.T;
                return g;
            }
        }
        public static PointD[] GetPoints(GeoJsonGeometry p,BioImage b)
        {
            if (p.type == "Point")
            {
                List<PointD> points = new List<PointD>();
                double[] gs = JsonConvert.DeserializeObject<double[]>(p.coordinates.ToString());
                points.Add(new PointD(gs[0], gs[1]));
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = new PointD(b.StageSizeX + (points[i].X / b.SizeX) * b.Volume.Width, b.StageSizeY + (points[i].Y / b.SizeY) * b.Volume.Height);
                }
                return points.ToArray();
            }
            else if(p.type == "Polygon")
            {
                List<PointD> points = new List<PointD>();
                double[][][] gs = JsonConvert.DeserializeObject<double[][][]>(p.coordinates.ToString());
                foreach (double[] item in gs[0])
                {
                    points.Add(new PointD(item[0], item[1]));
                }
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = new PointD(b.StageSizeX + (points[i].X / b.SizeX)*b.Volume.Width, b.StageSizeY + (points[i].Y / b.SizeY) * b.Volume.Height);
                }
                return points.ToArray();
            }
            else
            {
                List<PointD> points = new List<PointD>();
                double[][] gs = JsonConvert.DeserializeObject<double[][]>(p.coordinates.ToString());
                foreach (double[] item in gs)
                {
                    points.Add(new PointD(item[0], item[1]));
                }
                for (int i = 0; i < points.Count; i++)
                {
                    points[i] = new PointD(b.StageSizeX + (points[i].X / b.SizeX) * b.Volume.Width, b.StageSizeY + (points[i].Y / b.SizeY) * b.Volume.Height);
                }
                return points.ToArray();
            }
        }
        public class GeoJsonLineString
        {
            public class GeoJsonPlane
            {
                public int c { get; set; }
                public int z { get; set; }
                public int t { get; set; }
            }
            public string type { get; set; }
            public double[][] coordinates { get; set; }
            public GeoJsonPlane plane { get; set; }
            public static GeoJsonLineString FromROI(ROI roi, BioImage b)
            {
                GeoJsonLineString g = new GeoJsonLineString();
                double[][] ds = new double[roi.PointsD.Count][];
                for (int i = 0; i < roi.PointsD.Count; i++)
                {
                    PointD po = b.ToImageSpace(roi.PointsD[i]);
                    ds[i] = new double[2] { po.X, po.Y };
                }
                g.coordinates = ds;
                g.type = "LineString";
                g.plane = new GeoJsonPlane();
                g.plane.z = roi.coord.Z;
                g.plane.c = roi.coord.C;
                g.plane.t = roi.coord.T;
                return g;
            }
        }

        public class GeoJsonPolygon
        {
            public class GeoJsonPlane
            {
                public int c { get; set; }
                public int z { get; set; }
                public int t { get; set; }
            }
            public string type { get; set; }
            public double[][][] coordinates { get; set; }
            public GeoJsonPlane plane { get; set; }
            public static GeoJsonPolygon FromROI(ROI roi, BioImage b)
            {
                int pc = roi.PointsD.Count;
                if (roi.PointsD.Last() != roi.PointsD.First())
                    pc = roi.PointsD.Count + 1;
                GeoJsonPolygon g = new GeoJsonPolygon();
                double[][][] dds = new double[1][][];
                double[][] ds = new double[pc][];
                for (int i = 0; i < pc; i++)
                {
                    if (i >= roi.PointsD.Count)
                    {
                        PointD po = b.ToImageSpace(roi.PointsD[0]);
                        ds[i] = new double[2] { (int)po.X, (int)po.Y };
                    }
                    else
                    {
                        PointD po = b.ToImageSpace(roi.PointsD[i]);
                        ds[i] = new double[2] { (int)po.X, (int)po.Y };
                    }
                }
                
                dds[0] = ds;
                g.coordinates = dds;
                g.type = "Polygon";
                g.plane = new GeoJsonPlane();
                g.plane.z = roi.coord.Z;
                g.plane.c = roi.coord.C;
                g.plane.t = roi.coord.T;
                return g;
            }
        }

        public class Properties
        {
            public string ObjectType { get; set; }
            public bool IsLocked { get; set; }
        }

        public static void Save(string file, BioImage b)
        {
            string j = "{ \"type\":\"FeatureCollection\",\"features\":[";
            int i = 0;
            foreach (ROI roi in b.Annotations)
            {
                if(i==0)
                    j += "{\"type\":\"Feature\",\"geometry\":";
                else
                    j += ",{\"type\":\"Feature\",\"geometry\":";
                if (roi.type == ROI.Type.Point)
                {
                    GeoJsonPoint p = GeoJsonPoint.FromROI(roi,b);
                    j+= JsonConvert.SerializeObject(p);
                }
                else if (roi.type == ROI.Type.Rectangle || (roi.type == ROI.Type.Polygon && roi.closed) || roi.type == ROI.Type.Freeform)
                {
                    GeoJsonPolygon p = GeoJsonPolygon.FromROI(roi, b);
                    j += JsonConvert.SerializeObject(p);
                }
                else if (roi.type == ROI.Type.Polyline || roi.type == ROI.Type.Line || roi.type == ROI.Type.Polygon)
                {
                    GeoJsonLineString p = GeoJsonLineString.FromROI(roi, b);
                    j += JsonConvert.SerializeObject(p);
                }

                j += ",\"properties\":{\"object_type\":\"annotation\",\"isLocked\":false}}";
                i++;
            }
            j += "]}";
            File.WriteAllText(file,j);
        }

        public static ROI[] ReadROI(string filePath, BioImage b)
        {
            List<ROI> rois = new List<ROI>();
            string st = File.ReadAllText(filePath);
            GeoJsonFeatureCollection gs = JsonConvert.DeserializeObject<GeoJsonFeatureCollection>(st);

            foreach (GeoJsonFeature f in gs.features)
            {
                ROI r = new ROI();
                if(f.geometry.type == "Polygon")
                {
                    r.type = ROI.Type.Polygon;
                    r.closed = true;
                    r.AddPoints(GetPoints(f.geometry,b));
                    if(f.geometry.plane != null)
                        r.coord = f.geometry.GetZCT();
                }
                else if(f.geometry.type == "LineString")
                {
                    r.type = ROI.Type.Line;
                    r.AddPoints(GetPoints(f.geometry, b));
                    if (r.PointsD.Count > 2)
                        r.type = ROI.Type.Polyline;
                    if (f.geometry.plane != null)
                        r.coord = f.geometry.GetZCT();
                }
                else
                {
                    r.type = ROI.Type.Point;
                    r.AddPoints(GetPoints(f.geometry, b));
                    if (f.geometry.plane != null)
                        r.coord = f.geometry.GetZCT();
                }
                rois.Add(r);
            }
            return rois.ToArray();
        }
    }
}
