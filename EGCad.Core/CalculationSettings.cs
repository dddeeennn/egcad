using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGCad.Common.Infrastructure;
using EGCad.Core.Input;

namespace EGCad.Core
{
    public class CalculationSettings
    {
        public int AdditionalPointCount { get; private set; }

        public NormalizeType Normilize { get; private set; }

        public StatCalculationType StatCalculation { get; private set; }

        public CalculationSettings(GeoData settings)
        {
            AdditionalPointCount = settings.AdditionalPointCount;
            Normilize = settings.Normilize;
            StatCalculation = settings.StatCalculation;
        }
    }
}
