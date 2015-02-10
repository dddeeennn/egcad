using System.Collections.Generic;
using System.Linq;

namespace EGCad.Common.Model.Clusterize
{
    public class StatDistanceRow
    {
        public StatDistanceCell[] Cells { get; private set; }

        public StatDistanceRow(IEnumerable<StatDistanceCell> cells)
        {
            Cells = cells.ToArray();
        }
    }
}
