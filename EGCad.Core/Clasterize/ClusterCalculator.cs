using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;
using EGCad.Core.Normalize;

namespace EGCad.Core.Clasterize
{
    public class ClusterCalculator
    {
        private readonly IDataNormalizer _normalizer;
        private readonly StatDistanceTableProvider _statDistanceTableProvider;

        public ClusterCalculator(CalculationSettings settings)
        {
            _normalizer = NormalizerFactory.Create(settings);
            _statDistanceTableProvider = new StatDistanceTableProvider(settings);
        }

        public ClusterTree Clusterize(Data sourceData)
        {
            var normalizedData = _normalizer.Normalize(sourceData);
            return Clusterize(normalizedData);
        }

        public ClusterTree Clusterize(NormalizeData normalizedData)
        {
            var result = new List<ClusterNode>();
            var statDistanceTable = _statDistanceTableProvider.Get(normalizedData);

            result.AddRange(normalizedData.Rows.Select(row => new ClusterNode(row.RowIdx, 0)));

            while (normalizedData.Rows.Count() > 3)
            {
                ClusterizeIterate(ref normalizedData, ref statDistanceTable, result);
            }

            return new ClusterTree(result);
        }

        /// <summary>
        /// 1. Find cell of statistic distance table   
        /// 2. Remove rows that row's index and column's  index equal cell coords
        /// 3. Join removed rows and insert in statistic table 
        /// </summary>
        /// <param name="normalizedData"></param>
        /// <param name="statDistanceTable"></param>
        /// <param name="result"></param>
        private void ClusterizeIterate(ref NormalizeData normalizedData, ref StatDistanceTable statDistanceTable, List<ClusterNode> result)
        {
            var min = statDistanceTable.Min();

            var leftLeaf = result.First(x => x.JoinedClasters.ToStr() == min.I.ToStr());
            var rightLeaf = result.First(x => x.JoinedClasters.ToStr() == min.J.ToStr());

            result.Remove(leftLeaf);
            result.Remove(rightLeaf);

            result.Add(new ClusterNode(min.I.Concat(min.J), min.Value, new[] { leftLeaf, rightLeaf }));

            normalizedData = JoinCluster(normalizedData,  normalizedData.RowIdx(min.I), normalizedData.RowIdx(min.J));
            statDistanceTable = _statDistanceTableProvider.Get(normalizedData);
        }

        /// <summary>
        /// join cluster i and j, reduce normalize data
        /// </summary>
        /// <param name="normalizeData">source normalize data</param>
        /// <param name="oldIdx">old cluster</param>
        /// <param name="newIdx">cluster for join</param>
        private NormalizeData JoinCluster(NormalizeData normalizeData, int[] oldIdx, int[] newIdx)
        {
            var idxs = oldIdx.Concat(newIdx);
            var row1 = normalizeData.Rows.First(x => x.RowIdx.ToStr() == oldIdx.ToStr());
            var row2 = normalizeData.Rows.First(x => x.RowIdx.ToStr() == newIdx.ToStr());

            var valueCount = row1.Values.Length;
            var values = new double[valueCount];

            for (var i = 0; i < valueCount; i++)
            {
                values[i] = (row1.Values[i] + row2.Values[i]) / 2;
            }

            var newRow = new NormalizeDataRow(idxs, values);

            var rowList = normalizeData.Rows.ToList();
            rowList.Remove(row1);
            rowList.Remove(row2);
            rowList.Add(newRow);

            return new NormalizeData(rowList.ToArray());
        }
    }
}
