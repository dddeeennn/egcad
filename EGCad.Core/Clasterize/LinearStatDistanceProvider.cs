using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
	public class LinearStatDistanceProvider : StatDistanceProviderBase
	{
		public LinearStatDistanceProvider(): base(StatCalculationType.Linear){}

		public override double GetStatDistance(double[] row1, double[] row2)
		{
			return row1.Select((t, i) => t - row2[i]).Sum();
		}
	}
}
