using System.Linq;

namespace EGCad.Core.Normalize
{
    public class NormalizerFactory
    {
        private static readonly IDataNormalizer[] Normalizers =
        {
            new EukleadAveragedNormalizer(), 
            new ModularCenteredNormalizer(), 
            new ModularNormalizer()
        }; 

        public static IDataNormalizer Create(CalculationSettings settings)
        {
            return Normalizers.First(n => n.Type == settings.Normilize);
        }
    }
}
