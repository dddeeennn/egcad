using System.Collections.Generic;
using EGCad.Common.Infrastructure;
using EGCad.Core.InputData;

namespace EGCad.Core.NormalizeData
{
    public abstract class DataNormalizerBase : IDataNormalizer
    {
        public NormalizeType Type { get; protected set; }

        public Data Normalize(Data sourceData)
        {
            var result = new List<ParameterTableEntry>();
            var sourcePoints = sourceData.Points;
            foreach (var point in sourcePoints)
            {
                var normalizedParameters = new List<Parameter>();
                var z = GetZeroLevelFactor(point.Parameters.ToArray());
                var v = GetVariationRange(point.Parameters.ToArray());

                point.Parameters.ForEach(p =>
                {
                    var normalizeValue = (p.Value - z) / v;
                    p.Value = normalizeValue;
                    normalizedParameters.Add(p);
                });
                result.Add(new ParameterTableEntry(point.Id, point.X, point.Y, normalizedParameters));
            }
            return new Data(result);
        }

        public abstract double GetZeroLevelFactor(Parameter[] row);//натуральное значение нулевого уровня j-фактора 

        public abstract double GetVariationRange(Parameter[] row);//интервал варьирования j-фактором
    }
}
