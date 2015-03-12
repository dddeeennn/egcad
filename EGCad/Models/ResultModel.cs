using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;

namespace EGCad.Models
{
    public class ResultModel
    {
        public EGNetwork Network;
        private readonly ClusterTree _clusterTree;
        public Map Map { get; set; }

        public int ClusterCount
        {
            get
            {
                var pointWithMaxClusterIndex = Points.OrderByDescending(x => x.ClusterIndex).FirstOrDefault();
                return pointWithMaxClusterIndex != null ? pointWithMaxClusterIndex.ClusterIndex : 0;
            }
        }

        public ResultModel(Map map, EGNetwork network, ClusterTree clusterTree)
        {
            Network = network;
            _clusterTree = clusterTree;
            Map = map;
        }

        public DrawPoint[] Points
        {
            get { return GetPoints().ToArray(); }
        }

        private IEnumerable<DrawPoint> GetPoints()
        {
            return Network.Points.Select(p =>
                GetDrawPoint(p.Parameters.X, GetClusterIndex(p.VariabilityFuncValue.PointId), p.IsNew));
        }

        /// <summary>
        /// get point for draw at the canvas
        /// </summary>
        /// <param name="x">r in the polar coord system</param>
        /// <param name="clusterIdx">cluster index</param>
        /// <param name="isNew">new positioned point</param>
        /// <returns></returns>
        private DrawPoint GetDrawPoint(double x, byte clusterIdx, bool isNew)
        {
            int px, py;

            if (Map.End.X < Map.Start.X)
            {
                px = (int)(Map.Start.X - x / Map.ScaleKoef * Math.Cos(Math.Atan(Map.K)));
                py = (int)(Map.Start.Y - x / Map.ScaleKoef * Math.Sin(Math.Atan(Map.K)));
            }
            else
            {
                px = (int)(Map.Start.X + x / Map.ScaleKoef * Math.Cos(Math.Atan(Map.K)));
                py = (int)(Map.Start.Y + x / Map.ScaleKoef * Math.Sin(Math.Atan(Map.K)));
            }

            return new DrawPoint(px, py, GetTitle(x), clusterIdx, isNew);
        }

        private byte GetClusterIndex(int pointId)
        {
            var clusterIdx = _clusterTree.Children.FindIndex(cluster => cluster.JoinedClasters.Contains(pointId));
            return (byte)clusterIdx;
        }

        private string GetTitle(double x)
        {
            return string.Format("[{0} , 0]", x);
        }
    }
}