using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;
using EGCad.Core.Normalize;
using Newtonsoft.Json;

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
            var clusterInnerDistanceNormalized = new List<double>();
            var clusterOuterDistance = new List<double>();
            var clusterInnerDistance = new List<double>();
            var clusterDispersion = new List<double>();
            var sourceNormalizedData = JsonConvert.DeserializeObject<NormalizeData>(JsonConvert.SerializeObject(normalizedData));

            var statDistanceTable = _statDistanceTableProvider.Get(normalizedData);

            result.AddRange(normalizedData.Rows.Select(row => new ClusterNode(row.RowIdx, 0)));

            while (normalizedData.Rows.Length > _settings.ClusterCount)
            {
                statDistanceTables.Enqueue(statDistanceTable);
                ClusterizeIterate(ref normalizedData,
                sourceNormalizedData,
                ref statDistanceTable,
                result,
                statDistanceQualityKoef,
                clusterInnerDistance,
                clusterInnerDistanceNormalized,
                clusterOuterDistance,
                clusterDispersion);
            }

            return new ClusterTree(result,
            statDistanceTables,
            statDistanceQualityKoef.ToArray(),
            clusterInnerDistance.ToArray(),
            clusterInnerDistanceNormalized.ToArray(),
            clusterOuterDistance.ToArray(),
            clusterDispersion.ToArray());
        }

        /// <summary>
        /// 1. Find cell of statistic distance table 
        /// 2. Remove rows that row's index and column's index equal cell coords
        /// 3. Join removed rows and insert in statistic table 
        /// </summary>
        /// <param name="normalizedData"></param>
        /// <param name="sourceNormalizeData"></param>
        /// <param name="statDistanceTable"></param>
        /// <param name="result"></param>
        /// <param name="statDistanceQualityKoefs"></param>
        /// <param name="clusterOuterDistance"></param>
        /// <param name="clusterInnerDistance"></param>
        /// <param name="clusterInnerDistanceNormalized"></param>
        /// <param name="clusterDispersion"></param>
        private void ClusterizeIterate(ref NormalizeData normalizedData,
        NormalizeData sourceNormalizeData,
        ref StatDistanceTable statDistanceTable,
        List<ClusterNode> result,
        List<double> statDistanceQualityKoefs,
        List<double> clusterInnerDistance,
        List<double> clusterInnerDistanceNormalized,
        List<double> clusterOuterDistance,
        List<double> clusterDispersion)
        {
            var min = statDistanceTable.Min();

            var leftLeaf = result.First(x => x.JoinedClasters.ToStr() == min.I.ToStr());
            var rightLeaf = result.First(x => x.JoinedClasters.ToStr() == min.J.ToStr());

            result.Remove(leftLeaf);
            result.Remove(rightLeaf);

            result.Add(new ClusterNode(min.I.Concat(min.J), min.Value, new[] { leftLeaf, rightLeaf }));

            clusterDispersion.Add(GetClusterDispersion(normalizedData));

            normalizedData = JoinCluster(normalizedData, normalizedData.RowIdx(min.I), normalizedData.RowIdx(min.J), statDistanceQualityKoefs);
            statDistanceTable = _statDistanceTableProvider.Get(normalizedData);

            clusterOuterDistance.Add(GetClusterOuterDistance(statDistanceTable));
            clusterInnerDistance.Add(GetClusterInnerDistanceNormalized(normalizedData, sourceNormalizeData));
            clusterInnerDistanceNormalized.Add(GetClusterInnerDistance(normalizedData,
            sourceNormalizeData));
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

            statDistanceKoefs.Add(GetStatDistanceKoefByCluster(row1.Values, row2.Values, newRow.Values));

            var rowList = normalizeData.Rows.ToList();
            rowList.Remove(row1);
            rowList.Remove(row2);
            rowList.Add(newRow);

            return new NormalizeData(rowList.ToArray());
        }

        private double GetStatDistanceKoefByCluster(double[] previousLeftData, double[] previousRightData, double[] currentData)
        {
            var sum = previousLeftData.Select((t, i) => Math.Pow(t - currentData[i], 2)).Sum();
            return Math.Sqrt(sum);
        }

        private double GetClusterOuterDistance(StatDistanceTable statDistanceTable)
        {
            return statDistanceTable.Rows.Sum(row => row.Cells.Sum(x => x.Value) / (row.Cells.Length - 1)) / statDistanceTable.Rows.Length;
        }

        private double GetClusterInnerDistance(NormalizeData current, NormalizeData source)
        {
            var clusterCount = current.Rows.Length;
            return current.Rows.Sum(x => GetInnerDistance(x, source)) / clusterCount;
        }

        private double GetInnerDistance(NormalizeDataRow row, NormalizeData source)
        {
            var joinedPoints = row.RowIdx;
            return joinedPoints.Select(joinedPoint => row.SourceValues.Select((v, idx) => Math.Sqrt(Math.Pow(v - source.Rows[joinedPoint].SourceValues[idx], 2)))
            .Sum() / row.SourceValues.Length)
            .Sum() / joinedPoints.Length;
        }

        private double GetClusterInnerDistanceNormalized(NormalizeData current, NormalizeData source)
        {
            var clusterCount = current.Rows.Length;
            return current.Rows.Sum(x => GetInnerDistanceNormalized(x, source)) / clusterCount;
        }

        private double GetInnerDistanceNormalized(NormalizeDataRow row, NormalizeData source)
        {
            var joinedPoints = row.RowIdx;
            return joinedPoints.Select(joinedPoint => row.Values.Select((v, idx) => Math.Sqrt(Math.Pow(v - source.Rows[joinedPoint].Values[idx], 2)))
            .Sum() / row.Values.Length)
            .Sum() / joinedPoints.Length;
        }

        private double GetClusterDispersion(NormalizeData normalizeData)
        {
            var dispersionByParameter = normalizeData.Rows
            .Select(row => row.Values.Select((v, idx) => Math.Pow(GetMeanValue(normalizeData, idx) - v, 2)).ToArray())
            .ToArray();
            var sumDispersion = new double[normalizeData.Rows[0].Values.Length];
            for (var i = 0; i < normalizeData.Rows[0].Values.Length; i++)
            {
                sumDispersion[i] = dispersionByParameter.Sum(x => x[i]) / (normalizeData.Rows.Length - 1);
            }
            return sumDispersion.Sum();
        }

        private double GetMeanValue(NormalizeData normalizeData, int parameterIndex)
        {
            return normalizeData.Rows.Average(x => x.Values[parameterIndex]);
        }
    }
}