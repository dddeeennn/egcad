namespace EGCad.Core.Clasterize
{
    public class ClusterNode
    {
        public int[] JoinedClasters { get; private set; }

        public double StatDistance { get; private set; }

        public ClusterNode(int[] joinedCluster, double statisticDistance)
        {
            JoinedClasters = joinedCluster;
            StatDistance = statisticDistance;
        }
    }
}
