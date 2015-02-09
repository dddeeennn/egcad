using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Common.MathExt;

namespace EGCad.Core.Normalize
{
    public class ModularCenteredNormalizer : DataNormalizerBase
    {
        public ModularCenteredNormalizer()
            : base(NormalizeType.ModularCentered)
        {
        }

        protected override double GetZeroLevelFactor(double[] data)
        {
            return data.ArithmeticalMean();
        }

        protected override double GetVariationRange(double[] data)
        {
            return data.Max() - data.Min();
        }
    }
}
