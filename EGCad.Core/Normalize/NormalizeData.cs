namespace EGCad.Core.Normalize
{
    public class NormalizeData
    {
        public NormalizeDataRow[] Rows { get; set; }

        public NormalizeData(params NormalizeDataRow[] rows)
        {
            Rows = rows;
        }

    }
}
