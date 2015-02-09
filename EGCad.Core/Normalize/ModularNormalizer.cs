using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Normalize
{
    public class ModularNormalizer : DataNormalizerBase
    {
        public ModularNormalizer() : base(NormalizeType.Modular)
        {
        }

        protected override double GetZeroLevelFactor(double[] data)
        {
            return 0;
        }

        protected override double GetVariationRange(double[] data)
        {
            return data.Max();
        }
    }
}
