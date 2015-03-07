using System;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
	public class QuadEukleadDistanceProvider : StatDistanceProviderBase
	{
		public QuadEukleadDistanceProvider()
			: base(StatCalculationType.QuadEuclead)
		{
		}

		public override double GetStatDistance(double[] row1, double[] row2)
		{
			return row1.Select((t, i) => Math.Pow(t - row2[i], 2)).Sum();
		}
	}
}
