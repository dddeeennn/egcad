using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;
using EGCad.Core.PointPosition;
using EGCad.Core.VairiabilityCalc;

namespace EGCad.Core
{
    // ReSharper disable once InconsistentNaming
    public class EGNetworkBuilder
    {
        private readonly VariabilityCalculator _variabilityCalculator = new VariabilityCalculator();
        private readonly PointProvider _pointProvider;

        private readonly CalculationSettings _settings = new CalculationSettings(2, NormalizeType.EuklideanAveraged,
            StatCalculationType.Euclead, 3);

        public EGNetworkBuilder()
        {
            _pointProvider = new PointProvider(_settings);
        }

        public EGNetworkBuilder(CalculationSettings settings)
        {
            _settings = settings;
            _pointProvider = new PointProvider(_settings);
        }

        public EGNetwork Calculate(Data sourceData)
        {
            var points = _variabilityCalculator.GetVariabilityFunction(sourceData)
                                               .Select(v => new EGNetworkPoint(sourceData.Parameters, v, false)).ToList();

            var newPoints = _pointProvider.AllocationPoint(sourceData)
                                          .Select(v => new EGNetworkPoint(sourceData.Parameters, v, true));

            points.AddRange(newPoints);
            return new EGNetwork(points);
        }
    }
}
