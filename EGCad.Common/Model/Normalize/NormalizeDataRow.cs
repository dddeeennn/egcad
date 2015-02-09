namespace EGCad.Common.Model.Normalize
{
    public class NormalizeDataRow
    {
        public int[] RowIdx { get; set; }
        public double[] Values { get; set; }

        public NormalizeDataRow(int[] rowIdx, double[] values)
        {
            RowIdx = rowIdx;
            Values = values;
        }
    }
}
