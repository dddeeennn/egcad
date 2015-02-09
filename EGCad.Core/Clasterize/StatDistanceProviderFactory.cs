using System.Linq;

namespace EGCad.Core.Clasterize
{
    public class StatDistanceProviderFactory
    {
        private static readonly IStatDistanceProvider[] StatDistanceProviders =
        {
              new EukleadDistanceProvider(),
              new QuadEukleadDistanceProvider()
        };

        public static IStatDistanceProvider Create(CalculationSettings settings)
        {
            return StatDistanceProviders.First(n => n.Type == settings.StatCalculation);
        }
    }
}
