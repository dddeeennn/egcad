using EGCad.Common.Infrastructure;
using EGCad.Core;

namespace EGCad.Tests.Sample
{
    public static class CalculationSettingsSamples
    {
        public static CalculationSettings DefaultCalculationSettings
        {
            get { return new CalculationSettings(3, NormalizeType.EuklideanAveraged, StatCalculationType.Euclead, 2); }
        }

        public static CalculationSettings EucleadExtendedCalculationSettings
        {
            get { return new CalculationSettings(2, NormalizeType.EuklideanAveraged, StatCalculationType.QuadEuclead, 5); }
        }

        public static CalculationSettings ModularCalculationSettings
        {
            get
            {
                return new CalculationSettings(4, NormalizeType.Modular, StatCalculationType.Linear, 1);
            }
        }

        public static CalculationSettings ModularCenteredCalculationSettings
        {
            get
            {
                return new CalculationSettings(2, NormalizeType.ModularCentered, StatCalculationType.Chebishev, 6);
            }
        }

        public static CalculationSettings ManhattanCalculationSettings
        {
            get
            {
                return new CalculationSettings(5, NormalizeType.ModularCentered, StatCalculationType.Manhatten, 2);
            }
        }
    }
}
