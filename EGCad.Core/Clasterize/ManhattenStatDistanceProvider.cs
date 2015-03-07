using System;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
	public class ManhattenStatDistanceProvider : StatDistanceProviderBase
	{
		public ManhattenStatDistanceProvider()
			: base(StatCalculationType.Manhatten)
		{
		}

		public override double GetStatDistance(double[] row1, double[] row2)
		{
			return row1.Select((itemFromRow1, itemIdx) => Math.Abs(itemFromRow1 - row2[itemIdx])).Sum();
		}
	}
}
