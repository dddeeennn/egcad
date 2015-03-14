using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Model.Data;

namespace EGCad.Tests.Sample
{
	static class PointSamples
	{
		/// <summary>
		/// Gets small default points from test sample.
		/// </summary>
		/// <value>
		/// small default points from test sample
		/// </value>
		public static ParameterTableEntry[] SmallDefaultPoints
		{
			get
			{
				return new[]
		        {
		            new ParameterTableEntry(0, 0, GetParameters(1,6,0,3,1)),
		            new ParameterTableEntry(1, 0, GetParameters(2,2,2,2,2)),
		            new ParameterTableEntry(2, 0, GetParameters(6,5,5,6,6)),
		            new ParameterTableEntry(3, 0, GetParameters(8,8,6,9,8)),
		            new ParameterTableEntry(4, 0, GetParameters(4,12,3,21,9)),
		            new ParameterTableEntry(5, 0, GetParameters(5,1,12,2,12)),
		            new ParameterTableEntry(6, 0, GetParameters(11,10,15,18,13))
		        };
			}
		}

		public static ParameterTableEntry[] DefaultPoints
		{
			get
			{
				return new[]
		        {
					new ParameterTableEntry(0,100, GetParameters(-1.165,2.08,20.7,0.51,16.4,0.16,1)),
					new ParameterTableEntry(1,120, GetParameters(-1.199,2.07,22.4,0.56,16.4,0.162,1)),
					new ParameterTableEntry(2,130, GetParameters(-1.235,2.08,24.89,0.6,15.8,0.158,1)),
					new ParameterTableEntry(3,220, GetParameters(-1.45,2.08,25.5,0.62,14.9,0.153,0.98)),
					new ParameterTableEntry(4,340, GetParameters(-1.616,2.04,26.24,0.65,14,0.155,0.94)),
					new ParameterTableEntry(5,420, GetParameters(-1.814,2.7,27.2,0.78,12,0.18,0.87)),
					new ParameterTableEntry(6,520, GetParameters(-1.936,3.02,28.9,0.83,11.5,0.18,0.86)),
					new ParameterTableEntry(7,560, GetParameters(-2.145,2.94,31.2,0.93,11,0.2,0.79)),
					new ParameterTableEntry(8,620, GetParameters(-2.154,2.93,33.4,1.16,10.5,0.21,0.78)),
					new ParameterTableEntry(9,700, GetParameters(-2.173,2.95,36.6,1.13,10.3,0.23,0.78)),
					new ParameterTableEntry(10,764, GetParameters(-2.124,2.93,32.1,1.39,10,0.2,0.75)),
					new ParameterTableEntry(11,804, GetParameters(-2.169,2.86,37.4,1.47,10.5,0.205,0.78)),
					new ParameterTableEntry(12,884, GetParameters(-2.192,2.91,36.7,1.41,10.5,0.205,0.77)),
					new ParameterTableEntry(13,946, GetParameters(-2.166,2.9,36.3,1.54,10,0.2,0.75)),
					new ParameterTableEntry(14,992, GetParameters(-2.167,2.88,37.4,1.58,10,0.205,0.76)),
					new ParameterTableEntry(15,1030, GetParameters(-2.187,2.85,37.9,1.57,10,0.2,0.72)),
					new ParameterTableEntry(16,1050, GetParameters(-2.253,2.8,38.6,1.56,10.1,0.2,0.71)),
					new ParameterTableEntry(17,1080, GetParameters(-2.314,2.73,41.3,1.54,9.5,0.205,0.74)),
					new ParameterTableEntry(18,1092, GetParameters(-2.352,2.5,44.6,1.44,9.3,0.21,0.76)),
					new ParameterTableEntry(19,1098, GetParameters(-2.362,2.2,43.3,1.47,9.3,0.22,0.72)),
					new ParameterTableEntry(20,1104, GetParameters(-2.381,2,43.7,1.39,9,0.2,0.72)),
					new ParameterTableEntry(21,1130, GetParameters(-2.386,2.02,44.7,1.34,9.2,0.22,0.72)),
					new ParameterTableEntry(22,1192, GetParameters(-2.396,2.2,43.6,1.35,9.1,0.21,0.71)),
					new ParameterTableEntry(23,1242, GetParameters(-2.413,1.8,44.9,1.32,8.6,0.23,0.69)),
					new ParameterTableEntry(24,1304, GetParameters(-2.416,1.9,44.5,1.32,8.9,0.22,0.64)),
					new ParameterTableEntry(25,1360, GetParameters(-2.486,1.7,44.3,1.33,8.6,0.22,0.66)),
					new ParameterTableEntry(26,1490, GetParameters(-2.548,1.61,47.2,1.35,7.1,0.2,0.62)),
					new ParameterTableEntry(27,1580, GetParameters(-2.594,1.63,45.3,1.36,7.4,0.225,0.65)),
					new ParameterTableEntry(28,1680, GetParameters(-2.667,1.6,48.8,1.35,7,0.225,0.61))
				};
			}
		}

		/// <summary>
		/// gran composite points (domain-specific)
		/// </summary>

		public static ParameterTableEntry[] DefaultGranCompositionPoints
		{
			get
			{
				return new[]
		        {
		            new ParameterTableEntry(0, 0, GetParameters(1.12,41.21,15.67,30.79,4.02,7.19)),
		            new ParameterTableEntry(1, 0, GetParameters(0.79,30.97,14.78,34.13,9.01,10.32)),
		            new ParameterTableEntry(2, 0, GetParameters(0.64,29.79,10.81,40.89,7.02,10.85)),
		            new ParameterTableEntry(3, 0, GetParameters(0.54,20.11,13.62,49.56,8.16,8.01)),
		            new ParameterTableEntry(4, 0, GetParameters(0.4,15.7,11.4,54.9,9.07,8.53)),
		            new ParameterTableEntry(5, 0, GetParameters(0.32,11.8,10.7,61.65,6.08,9.45)),
		            new ParameterTableEntry(6, 0, GetParameters(0.25,9.81,11.01,57.78,11.8,9.35)),
		            new ParameterTableEntry(7, 0, GetParameters(0.22,7.34,12.3,61.35,10.3,8.49)),
		            new ParameterTableEntry(8, 0, GetParameters(0.12,5.27,4.22,68.1,11.7,10.59)),
		            new ParameterTableEntry(9, 0, GetParameters(0.02,0.89,5.08,65.19,14.3,14.52)),
		            new ParameterTableEntry(10, 0, GetParameters(0,0.65,2.1,32.12,42.98,22.15)),
		            new ParameterTableEntry(11, 0, GetParameters(0,0.43,1.78,32.34,45.78,19.67)),
		            new ParameterTableEntry(12, 0, GetParameters(0,0.21,1.59,23.78,49.87,24.55)),
		            new ParameterTableEntry(13, 0, GetParameters(0,0.31,1.91,29.76,50.45,17.57)),
		            new ParameterTableEntry(14, 0, GetParameters(0,0.2,1.62,21.2,49.92,27.06)),
		            new ParameterTableEntry(15, 0, GetParameters(0,0.15,1.65,22.78,47.89,27.53)),
		            new ParameterTableEntry(16, 0, GetParameters(0,0.1,1.6,20.3,45.24,32.76)),
		            new ParameterTableEntry(17, 0, GetParameters(0,0.05,1.55,14.82,42.59,40.99)),
		            new ParameterTableEntry(18, 0, GetParameters(0,0.1,1.2,12.8,41.56,44.34)),
		            new ParameterTableEntry(19, 0, GetParameters(0,0.09,0.14,12.5,29.45,57.82)),
		            new ParameterTableEntry(20, 0, GetParameters(0,0.05,0.34,11,32.7,55.91)),
		            new ParameterTableEntry(21, 0, GetParameters(0,0.01,0.02,10.02,37.98,51.97)),
		            new ParameterTableEntry(22, 0, GetParameters(0,0,0,8.68,21.81,69.51))
		        };
			}
		}

		private static IList<Parameter> GetParameters(params double[] values)
		{
			return values.Select(value => new Parameter((int)value, value + " name", value + " unit", value)).ToArray();
		}
	}
}
