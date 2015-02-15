using System;

namespace EGCad.Core.VairiabilityCalc
{
	public class VariabilityFuncItem
	{
		public double X { get; private set; }

		public double Y { get; private set; }

		public double R
		{
			get { return Math.Round(Math.Sqrt(X * X + Y * Y), 2); }
		}

		public double VariabilityValue { get; set; }

		public VariabilityFuncItem(double x, double y, double variabilityValue)
		{
			X = x;
			Y = y;
			VariabilityValue = variabilityValue;
		}
	}
}
