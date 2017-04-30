namespace EGCad.Common.Model.Normalize
{
    public class NormalizeDataRow
    {
        public int[] RowIdx { get; set; }
        public double[] Values { get; set; }
        public double[] SourceValues { get; set; }

        public NormalizeDataRow(int[] rowIdx, double[] values, double[] sourceValues)
        {
            RowIdx = rowIdx;
            Values = values;
            SourceValues = sourceValues;
        }
    }
}
