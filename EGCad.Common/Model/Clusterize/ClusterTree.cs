using System.Collections.Generic;
using System.Linq;

namespace EGCad.Common.Model.Clusterize
{
	public class ClusterTree
	{
		public double StatDistance { get; set; }
		public List<ClusterNode> Children { get; set; }
        public Queue<StatDistanceTable> StatDistanceTables { get; set; }
        public double[] StatDistanceQualityKoef { get; set; }

		public ClusterTree()
		{
			StatDistance = 10;
			Children = new List<ClusterNode>();
            StatDistanceTables = new Queue<StatDistanceTable>();
            StatDistanceQualityKoef = new double[0];
		}

		public ClusterTree(List<ClusterNode> children, Queue<StatDistanceTable> statDistanceTables, double[] statDistanceQualityKoefs)
			: this()
		{
			Children = children.ToList();
		    StatDistanceTables = statDistanceTables;
			StatDistance = children.Select(node => node.StatDistance).Max();
		    StatDistanceQualityKoef = statDistanceQualityKoefs;
		}
	}
}
