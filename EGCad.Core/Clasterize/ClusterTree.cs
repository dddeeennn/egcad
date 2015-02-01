using System.Collections.Generic;
using System.Linq;

namespace EGCad.Core.Clasterize
{
	public class ClusterTree
	{
		public string Name { get; set; }
		public double StatDistance { get; set; }
		public List<ClusterNode> Children { get; set; }

		public ClusterTree()
		{
			Name = "root";
			StatDistance =10;
			Children = new List<ClusterNode>();
		}

		public ClusterTree(List<ClusterNode> children)
			: this()
		{
			Children = children.ToList();
			StatDistance = children.Select(node => node.StatDistance).Max();
		}

		public ClusterTree(string name, double y, List<ClusterNode> children)
		{
			Name = name;
			StatDistance = y;
			Children = children;
		}
	}
}
