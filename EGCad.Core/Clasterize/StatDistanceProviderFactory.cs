using System.Collections.Generic;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
    public class StatDistanceProviderFactory
    {
        private static readonly Dictionary<StatCalculationType, IStatDistanceProvider> Providers = new Dictionary<StatCalculationType, IStatDistanceProvider>()
        {
            {StatCalculationType.Euclead, new EukleadDistanceProvider()},
            {StatCalculationType.QuadEuclead, new QuadEukleadDistanceProvider()},

        };

        public static IStatDistanceProvider Create(StatCalculationType type)
        {
            return Providers.ContainsKey(type) ? Providers[type] : Providers[StatCalculationType.Euclead];
        }
    }
}
