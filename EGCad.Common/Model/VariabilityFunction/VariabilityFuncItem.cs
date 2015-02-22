using System;

namespace EGCad.Common.Model.VariabilityFunction
{
    public class VariabilityFuncItem
    {
        private readonly double _x0;
        public int PointId { get; private set; }

        public double X { get; private set; }

        public double R
        {
            get { return Math.Round(Math.Sqrt(Math.Pow(X - _x0, 2)), 2); }
        }

        public double VariabilityValue { get; set; }

        public VariabilityFuncItem(double x0, double x, int pointId, double variabilityValue)
        {
            X = x;
            VariabilityValue = variabilityValue;
            PointId = pointId;
            _x0 = x0;
        }
    }
}
