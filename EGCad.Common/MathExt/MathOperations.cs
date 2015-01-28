using System.Linq;

namespace EGCad.Common.MathExt
{
    public static class MathOperations
    {
        public static double ArithmeticalMean(this double[] series)
        {
            return series.Any() ? series.Sum() / series.Length : 0;
        }
    }
}
