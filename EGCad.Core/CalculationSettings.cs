using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;

namespace EGCad.Core
{
    public class CalculationSettings
    {
        public int AdditionalPointCount { get; private set; }

        public NormalizeType Normilize { get; private set; }

        public StatCalculationType StatCalculation { get; private set; }

        public int ClusterCount { get; private set; }

        internal CalculationSettings(GeoData settings)
            : this(settings.AdditionalPointCount, settings.Normilize, settings.StatCalculation,
            settings.ClusterCount)
        {
        }

        public CalculationSettings(int additionalPointCount, NormalizeType normalizeType,
            StatCalculationType statCalculationType, int clusterCount)
        {
            AdditionalPointCount = additionalPointCount;
            Normilize = normalizeType;
            StatCalculation = statCalculationType;
            ClusterCount = clusterCount;
        }
    }
}
