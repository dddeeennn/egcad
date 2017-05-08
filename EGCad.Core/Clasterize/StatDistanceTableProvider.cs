using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Normalize;

namespace EGCad.Core.Clasterize
{
    public class StatDistanceTableProvider
    {
        private readonly CalculationSettings _settings;

        public StatDistanceTableProvider(CalculationSettings settings)
        {
            _settings = settings;
        }

        public StatDistanceTable Get(NormalizeData normalizedData)
        {
            return new StatDistanceTable(normalizedData.Rows.Select(row => 
                   new StatDistanceRow(normalizedData.Rows.Select(col => GetStatDistanceCell(normalizedData, row.RowIdx, col.RowIdx))
                                    )).ToArray());
        }

        private StatDistanceCell GetStatDistanceCell(NormalizeData data, int[] i, int[] j)
        {
            var statDistanceProvider = StatDistanceProviderFactory.Create(_settings);

            return new StatDistanceCell(i, j, statDistanceProvider.GetStatDistance( data.RowValues(i), data.RowValues(j)));
        }
    }
}
