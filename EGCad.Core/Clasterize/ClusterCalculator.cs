using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;
using EGCad.Core.Normalize;

namespace EGCad.Core.Clasterize
{
    public class ClusterCalculator
    {
        private readonly CalculationSettings _settings;
        private readonly IDataNormalizer _normalizer;
        private readonly StatDistanceTableProvider _statDistanceTableProvider;

        public ClusterCalculator(CalculationSettings settings)
        {
            _settings = settings;
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
            var statDistanceTables = new Queue<StatDistanceTable>();
            var statDistanceQualityKoef = new List<double>();

            var statDistanceTable = _statDistanceTableProvider.Get(normalizedData);

            result.AddRange(normalizedData.Rows.Select(row => new ClusterNode(row.RowIdx, 0)));

            while (normalizedData.Rows.Count() > _settings.ClusterCount)
            {
                statDistanceTables.Enqueue(statDistanceTable);
                ClusterizeIterate(ref normalizedData, ref statDistanceTable, result, statDistanceQualityKoef);
            }

            return new ClusterTree(result, statDistanceTables, statDistanceQualityKoef.ToArray());
        }

        /// <summary>
        /// 1. Find cell of statistic distance table   
        /// 2. Remove rows that row's index and column's  index equal cell coords
        /// 3. Join removed rows and insert in statistic table 
        /// </summary>
        /// <param name="normalizedData"></param>
        /// <param name="statDistanceTable"></param>
        /// <param name="result"></param>
        /// <param name="statDistanceQualityKoefs"></param>
        private void ClusterizeIterate(ref NormalizeData normalizedData, ref StatDistanceTable statDistanceTable,
            List<ClusterNode> result, List<double> statDistanceQualityKoefs)
        {
            var min = statDistanceTable.Min();

            var leftLeaf = result.First(x => x.JoinedClasters.ToStr() == min.I.ToStr());
            var rightLeaf = result.First(x => x.JoinedClasters.ToStr() == min.J.ToStr());

            result.Remove(leftLeaf);
            result.Remove(rightLeaf);

            result.Add(new ClusterNode(min.I.Concat(min.J), min.Value, new[] { leftLeaf, rightLeaf }));

            normalizedData = JoinCluster(normalizedData, normalizedData.RowIdx(min.I), normalizedData.RowIdx(min.J), statDistanceQualityKoefs);
            statDistanceTable = _statDistanceTableProvider.Get(normalizedData);
        }

        /// <summary>
        /// join cluster i and j, reduce normalize data
        /// </summary>
        /// <param name="normalizeData">source normalize data</param>
        /// <param name="oldIdx">old cluster</param>
        /// <param name="newIdx">cluster for join</param>
        /// <param name="statDistanceKoefs"></param>
        private NormalizeData JoinCluster(NormalizeData normalizeData, int[] oldIdx, int[] newIdx, List<double> statDistanceKoefs)
        {
            var idxs = oldIdx.Concat(newIdx);
            var row1 = normalizeData.Rows.First(x => x.RowIdx.ToStr() == oldIdx.ToStr());
            var row2 = normalizeData.Rows.First(x => x.RowIdx.ToStr() == newIdx.ToStr());

            var valueCount = row1.Values.Length;
            var values = new double[valueCount];
            var sorceValues = new double[valueCount];

            for (var i = 0; i < valueCount; i++)
            {
                values[i] = (row1.Values[i] + row2.Values[i]) / 2;
                sorceValues[i] = (row1.SourceValues[i] + row2.SourceValues[i]) / 2;
            }

            var newRow = new NormalizeDataRow(idxs, values, sorceValues);

            statDistanceKoefs.Add(GetStatDistanceKoefByCluster(row1.SourceValues, row2.SourceValues, newRow.SourceValues));

            var rowList = normalizeData.Rows.ToList();
            rowList.Remove(row1);
            rowList.Remove(row2);
            rowList.Add(newRow);

            return new NormalizeData(rowList.ToArray());
        }

        private double GetStatDistanceKoefByCluster(double[] previousLeftData, double[] previousRightData, double[] currentData)
        {
            var parametersLength = previousLeftData.Length;
            Double result = 0;

            for (var i = 0; i < parametersLength; i++)
            {
                result += Math.Abs(currentData[i] - previousLeftData[i]) / (previousLeftData[i] + previousRightData[i]);
            }

            return result / parametersLength;
        }
    }
}
