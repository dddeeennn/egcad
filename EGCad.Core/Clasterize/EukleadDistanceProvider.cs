using System;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
    public class EukleadDistanceProvider : StatDistanceProviderBase
    {
        public EukleadDistanceProvider() : base(StatCalculationType.Euclead) { }

        public override double GetStatDistance(double[] row1, double[] row2)
        {
            var sum = row1.Select((t, i) => Math.Pow(t - row2[i], 2)).Sum();
            return Math.Sqrt(sum);
        }
    }
}
