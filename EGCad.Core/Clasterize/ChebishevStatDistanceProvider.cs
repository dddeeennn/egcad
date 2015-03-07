using System;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
	public class ChebishevStatDistanceProvider : StatDistanceProviderBase
	{
		public ChebishevStatDistanceProvider()
			: base(StatCalculationType.Chebishev)
		{
		}

		public override double GetStatDistance(double[] row1, double[] row2)
		{
			return row1.Select((item1, idx1) => Math.Abs(item1 - row2[idx1])).Max();
		}
	}
}
