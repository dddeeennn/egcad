using System;
using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Common.MathExt;

namespace EGCad.Core.Normalize
{
    public class EukleadAveragedNormalizer : DataNormalizerBase
    {
        public EukleadAveragedNormalizer()
            : base(NormalizeType.EuklideanAveraged)
        {
        }

        public override double GetZeroLevelFactor(double[] data)
        {
            return data.ArithmeticalMean();
        }

        public override double GetVariationRange(double[] data)
        {
            return Math.Sqrt(data.Select(val => val * val).Sum()) / data.Length;
        }
    }
}
