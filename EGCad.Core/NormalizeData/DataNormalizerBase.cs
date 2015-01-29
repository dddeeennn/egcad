using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Core.InputData;

namespace EGCad.Core.NormalizeData
{
    public abstract class DataNormalizerBase : IDataNormalizer
    {
        public NormalizeType Type { get; protected set; }

        protected DataNormalizerBase(NormalizeType type)
        {
            Type = type;
        }

        public Data Normalize(Data sourceData)
        {
            var result = new List<ParameterTableEntry>();

            if (!sourceData.Points.Any()) return new Data(result);

            var sourceColumns = GetPreprocessedColumns(sourceData);

            foreach (var point in sourceData.Points)
            {
                var normalizedParameters = new List<Parameter>();

                for (var i = 0; i < point.Parameters.Count; i++)
                {
                    var p = point.Parameters[i];
                    var z = GetZeroLevelFactor(sourceColumns[i]);
                    var v = GetVariationRange(sourceColumns[i]);
                    p.Value = (p.Value - z) / v;
                    normalizedParameters.Add(p);
                }
                result.Add(new ParameterTableEntry(point.Id, point.X, point.Y, normalizedParameters));
            }
            return new Data(result);
        }

        private static double[][] GetPreprocessedColumns(Data sourceData)
        {
            var paramCount = sourceData.Points[0].Parameters.Count;

            var sourceColumns = new Double[paramCount][];

            for (var i = 0; i < paramCount; i++)
            {
                sourceColumns[i] = sourceData.Points.SelectMany(po => po.Parameters)
                    .Where(param => param.Id == sourceData.Points[0].Parameters[i].Id)
                    .Select(par => par.Value).ToArray();
            }
            return sourceColumns;
        }

        public abstract double GetZeroLevelFactor(double[] data);//натуральное значение нулевого уровня j-фактора 

        public abstract double GetVariationRange(double[] data);//интервал варьирования j-фактором
    }
}
