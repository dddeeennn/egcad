using System.Collections.Generic;
using System.Linq;

namespace EGCad.Common.Model.Clusterize
{
    public class ClusterTree
    {
        public string Name { get; set; }
        public double StatDistance { get; set; }
        public List<ClusterNode> Children { get; set; }
        public Queue<StatDistanceTable> StatDistanceTables { get; set; }
        public double[] StatDistanceQualityKoef { get; set; }
        public double[] ClusterOuterDistance { get; set; }
        public double[] ClusterInnerDistance { get; set; }

        public ClusterTree()
        {
            Name = "root";
            StatDistance = 10;
            Children = new List<ClusterNode>();
            StatDistanceTables = new Queue<StatDistanceTable>();
            StatDistanceQualityKoef = new double[0];
            ClusterInnerDistance = new double[0];
            ClusterOuterDistance = new double[0];
        }

        public ClusterTree(List<ClusterNode> children,
            Queue<StatDistanceTable> statDistanceTables,
            double[] statDistanceQualityKoefs,
            double[] clusterOuterDistance,
            double[] clusterInnerDistance)
            : this()
        {
            Children = children.ToList();
            StatDistanceTables = statDistanceTables;
            StatDistance = children.Select(node => node.StatDistance).Max();
            StatDistanceQualityKoef = statDistanceQualityKoefs;
            ClusterInnerDistance = clusterInnerDistance;
            ClusterOuterDistance = clusterOuterDistance;
        }
    }
}
