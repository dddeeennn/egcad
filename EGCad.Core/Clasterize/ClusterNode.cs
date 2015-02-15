using System;
using EGCad.Common.Extensions;

namespace EGCad.Core.Clasterize
{
	public class ClusterNode : IComparable<ClusterNode>
	{
		public ClusterNode(int[] joinedCluster, double statisticDistance)
		{
			JoinedClasters = joinedCluster;
			StatDistance = statisticDistance;
		}

		public ClusterNode(int[] joinedCluster, double statisticDistance, ClusterNode[] children)
			: this(joinedCluster, statisticDistance)
		{
			Children = children;
		}

		public int[] JoinedClasters { get; private set; }

		public double StatDistance { get; private set; }

		public ClusterNode[] Children { get; set; }

		public string Name
		{
			get { return JoinedClasters.ToStr(); }
		}

		public int CompareTo(ClusterNode other)
		{
			return JoinedClasters.Length > other.JoinedClasters.Length ?
				 1 : JoinedClasters.Length < other.JoinedClasters.Length ?
				-1 : 0;
		}
	}
}
