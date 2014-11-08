using EGCad.Common.Infrastructure;

namespace EGCad.Models
{
    public class CalculationParameterModel
    {
        public int AdditionalPointCount { get; set; }

        public NormalizeType Normilize { get; set; }

        public StatCalculationType StatCalculation { get; set; }
    }
}