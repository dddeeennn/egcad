using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
    public abstract class StatDistanceProviderBase : IStatDistanceProvider
    {
        protected StatDistanceProviderBase(StatCalculationType type)
        {
            Type = type;
        }

        public abstract double GetStatDistance(double[] row1, double[] row2);

        public StatCalculationType Type { get; private set; }
    }
}
