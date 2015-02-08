using System;

namespace EGCad.Core.VairiabilityCalc
{
    public class VariabilityFuncItem
    {
        private double _x0, _y0;

        public double X { get; private set; }

        public double Y { get; private set; }

        public double R
        {
            get { return Math.Round(Math.Sqrt(Math.Pow(X - _x0, 2) + Math.Pow(Y - _y0, 2)), 2); }
        }

        public double VariabilityValue { get; set; }

        public VariabilityFuncItem(double x0, double y0, double x, double y, double variabilityValue)
        {
            X = x;
            Y = y;
            VariabilityValue = variabilityValue;
            _y0 = y0;
            _x0 = x0;
        }
    }
}
