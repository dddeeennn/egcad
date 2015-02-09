namespace EGCad.Common.Model.Clusterize
{
    public class StatDistanceCell
    {
        public int[] I { get; set; }
        public int[] J { get; set; }
        public double Value { get; set; }

        public StatDistanceCell(int[] i, int[] j, double value)
        {
            I = i;
            J = j;
            Value = value;
        }
    }
}
