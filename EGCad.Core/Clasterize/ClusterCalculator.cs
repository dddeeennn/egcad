using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Infrastructure;
using EGCad.Core.Normalize;

namespace EGCad.Core.Clasterize
{
    public class ClusterCalculator
    {
        private readonly StatCalculationType _calculationType;

        private Data _data;

        public ClusterCalculator(StatCalculationType calculationType)
        {
            _calculationType = calculationType;
        }

        public ClusterNode[] Clusterize(Data data)
        {
            _data = data;

            var result = new List<ClusterNode>();
            var normalizedData = GetNormalizedData(data);
            var statDistanceTable = GetStatDistanceTable(normalizedData);

            while (normalizedData.Rows.Count() > 3)
            {
                ClusterizeIterate(ref normalizedData, ref statDistanceTable, result);
            }

            return result.ToArray();
        }

        private NormalizeData GetNormalizedData(Data data)
        {
            var rows =
                data.Points.Select(
                    point =>
                        new NormalizeDataRow(new[] { point.Id },
                                             point.Parameters.Select(param => param.Value).ToArray()))
                    .ToArray();
            return new NormalizeData(rows);
        }

        private void ClusterizeIterate(ref NormalizeData normalizedData, ref StatDistanceTable statDistanceTable, List<ClusterNode> result)
        {
            var min = statDistanceTable.Min();

            result.Add(new ClusterNode(min.I.Concat(min.J), min.Value));

            var row1Idx = normalizedData.Rows.First(r => r.RowIdx.ToStr() == min.I.ToStr()).RowIdx;
            var row2Idx = normalizedData.Rows.First(r => r.RowIdx.ToStr() == min.J.ToStr()).RowIdx;

            normalizedData = JoinCluster(normalizedData, row1Idx, row2Idx);
            statDistanceTable = GetStatDistanceTable(normalizedData);
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
            var rows = _data.Points.Where(point => idxs.Contains(point.Id)).ToArray();

            var values = new double[rows[0].Parameters.Count()];

            for (var i = 0; i < rows[0].Parameters.Count(); i++)
            {
                var val = 0d;
                Array.ForEach(rows, row => val += row.Parameters[i].Value);
                values[i] = val / rows.Length;
            }

            var newRow = new NormalizeDataRow(idxs, values);
            var rowList = normalizeData.Rows.ToList();
            rowList.Remove(normalizeData.Rows.First(x => x.RowIdx.ToStr() == oldIdx.ToStr()));
            rowList.Remove(normalizeData.Rows.First(x => x.RowIdx.ToStr() == newIdx.ToStr()));
            rowList.Add(newRow);
            return new NormalizeData(rowList.ToArray());
        }

        private StatDistanceTable GetStatDistanceTable(NormalizeData normalizedData)
        {
            var rows = GetStatDistanceRows(normalizedData);
            return new StatDistanceTable(rows);
        }

        private StatDistanceRow[] GetStatDistanceRows(NormalizeData normalizeData)
        {
            var result = new List<StatDistanceRow>();

            for (var i = 0; i < normalizeData.Rows.Length; i++)
            {
                var row =
                    new StatDistanceRow(
                        normalizeData.Rows.Where((t, j) => j > i).Select(t =>
                                GetStatDistanceCell(normalizeData, normalizeData.Rows[i].RowIdx, t.RowIdx))
                                                                 .ToArray());
                result.Add(row);
            }

            return result.ToArray();
        }

        private StatDistanceCell GetStatDistanceCell(NormalizeData data, int[] i, int[] j)
        {
            var statDistanceProvider = StatDistanceProviderFactory.Create(_calculationType);

            var row1Values = data.Rows.First(x => x.RowIdx.ToStr() == i.ToStr()).Values;
            var row2Values = data.Rows.First(x => x.RowIdx.ToStr() == j.ToStr()).Values;
            return new StatDistanceCell(i, j, statDistanceProvider.GetStatDistance(row1Values, row2Values));
        }
    }
}
