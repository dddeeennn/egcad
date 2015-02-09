namespace EGCad.Common.Model.Clusterize
{
    public class StatDistanceRow
    {
        public StatDistanceCell[] Cells { get; private set; }

        public StatDistanceRow(StatDistanceCell[] cells)
        {
            Cells = cells;
        }
    }
}
