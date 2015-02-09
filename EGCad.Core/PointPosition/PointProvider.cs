﻿using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.VariabilityFunction;
using EGCad.Core.Clasterize;
using EGCad.Core.Normalize;
using EGCad.Core.VairiabilityCalc;

namespace EGCad.Core.PointPosition
{
    public class PointProvider
    {
        private readonly IDataNormalizer _normalizer;
        private readonly ClusterCalculator _clusterCalculator;
        private readonly VariabilityCalculator _variabilityCalculator = new VariabilityCalculator();

        public PointProvider(CalculationSettings settings)
        {
            _normalizer = NormalizerFactory.Create(settings);
            _clusterCalculator = new ClusterCalculator(settings);
        }

        public VariabilityFuncItem[] AllocationPoint(Data sourceData)
        {
            var result = new List<VariabilityFuncItem>();

            var variabilityFunc = _variabilityCalculator.GetVariabilityFunction(sourceData);

            var normalizedData = _normalizer.Normalize(sourceData);

            var clusterizedData = _clusterCalculator.Clusterize(normalizedData);

            var interClasterPoints = GetInterclasterPoint(clusterizedData.Children.ToArray(), variabilityFunc, sourceData);

            result.AddRange(interClasterPoints.Select((p,index) =>
            {
                var x = (p.Item1.X + p.Item2.X) / 2;
                var y = (p.Item1.Y + p.Item2.Y) / 2;
                var variabilityValue = (variabilityFunc.First(v1 => v1.PointId == p.Item1.Id).VariabilityValue +
                                       variabilityFunc.First(v2 => v2.PointId == p.Item2.Id).VariabilityValue)/2;
                return new VariabilityFuncItem(sourceData.Points[0].X,
                    sourceData.Points[0].Y, x, y, sourceData.Points.Count() + index, variabilityValue);
            }).ToArray());

            return result.ToArray();
        }

        private IEnumerable<Tuple<ParameterTableEntry, ParameterTableEntry>> GetInterclasterPoint(ClusterNode[] clusters, VariabilityFuncItem[] variabilityFunc, Data sourceData)
        {
            var result = new List<Tuple<ParameterTableEntry, ParameterTableEntry>>();
            for (int i = 0, j = 1; j < variabilityFunc.Length; i++, j++)
            {
                var cluster1 = GetCluster(clusters, variabilityFunc[i].PointId);
                var cluster2 = GetCluster(clusters, variabilityFunc[j].PointId);

                if (cluster1 != cluster2)
                {
                    result.Add(new Tuple<ParameterTableEntry, ParameterTableEntry>(
                        sourceData.Points.First(point => point.Id == variabilityFunc[i].PointId),
                        sourceData.Points.First(point => point.Id == variabilityFunc[j].PointId)));
                }
            }
            return result.ToArray();
        }

        private ClusterNode GetCluster(IEnumerable<ClusterNode> clusters, int pointId)
        {
            return clusters.First(c => c.JoinedClasters.Contains(pointId));
        }
    }
}