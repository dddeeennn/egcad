using System;
using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Common.MathExt;
using EGCad.Core.InputData;

namespace EGCad.Core.NormalizeData
{
    public class EukleadAveragedNormalizer : DataNormalizerBase
    {
        public EukleadAveragedNormalizer()
        {
            Type = NormalizeType.EuklideanAveraged;
        }

        public override double GetZeroLevelFactor(Parameter[] row)
        {
            return row.Select(p => p.Value).ToArray().ArithmeticalMean();
        }

        public override double GetVariationRange(Parameter[] row)
        {
            var series = row.Select(p => p.Value * p.Value).ToArray();
            return Math.Sqrt(series.Sum()) / row.Length;
        }
    }
}
