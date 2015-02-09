namespace EGCad.Common.Model.Normalize
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
