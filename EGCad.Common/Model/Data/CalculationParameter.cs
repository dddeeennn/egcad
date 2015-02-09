using EGCad.Common.Infrastructure;

namespace EGCad.Common.Model.Data
{
    public class CalculationParameter
    {
        public int AdditionalPointCount { get; set; }

        public NormalizeType Normilize { get; set; }

        public StatCalculationType StatCalculation { get; set; }

        public int ClusterCount { get; set; }
    }
}