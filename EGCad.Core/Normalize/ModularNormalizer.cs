using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Normalize
{
    public class ModularNormalizer : DataNormalizerBase
    {
        public ModularNormalizer() : base(NormalizeType.Modular)
        {
        }

        public override double GetZeroLevelFactor(double[] data)
        {
            return 0;
        }

        public override double GetVariationRange(double[] data)
        {
            return data.Max();
        }
    }
}
