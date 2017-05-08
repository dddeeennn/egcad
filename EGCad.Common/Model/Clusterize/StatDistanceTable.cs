using System;
using System.Linq;

namespace EGCad.Common.Model.Clusterize
{
    public class StatDistanceTable
    {
        private const double Tolerance = 0.00000000000001;

        public StatDistanceRow[] Rows { get; private set; }

        public StatDistanceTable(StatDistanceRow[] rows)
        {
            Rows = rows;
        }

        public StatDistanceCell Min()
        {
            var cells = Rows.SelectMany(row => row.Cells).ToArray();
            var min = cells.Where(cell => cell.Value > 0).Select(cell => cell.Value).Min();
            return cells.First(c => Math.Abs(c.Value - min) < Tolerance);
        }
    }
}
