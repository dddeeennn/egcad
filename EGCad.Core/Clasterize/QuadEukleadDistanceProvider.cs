using System;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
    public class QuadEukleadDistanceProvider:StatDistanceProviderBase
    {
        public QuadEukleadDistanceProvider() : base(StatCalculationType.QuadEuclead)
        {
        }

        public override double GetStatDistance(double[] row1, double[] row2)
        {
            throw new NotImplementedException();
        }
    }
}
