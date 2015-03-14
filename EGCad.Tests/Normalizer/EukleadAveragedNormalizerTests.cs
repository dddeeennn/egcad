using EGCad.Common.Model.Data;
using EGCad.Core.Normalize;
using EGCad.Tests.Sample;
using NUnit.Framework;

namespace EGCad.Tests.Normalizer
{
	[TestFixture]
	public class EukleadAveragedNormalizerTests
	{
		private EukleadAveragedNormalizer _normalizer;
		[SetUp]
		public void SetUps()
		{
			_normalizer = new EukleadAveragedNormalizer();
		}

		[Test]
		public void EukleadAveragedNormalizerShouldCorrectlyNormalize()
		{
			var expected = NormalizedDataSamples.DefaultNormalizedData;
		}

		[Test]
		public void EukleadAveragedNormalizerShouldCorrectlyNormalizeGranCompositionSample()
		{
			var expected = NormalizedDataSamples.GranCompositionNormalizedData;

			var data = new Data(PointSamples.DefaultGranCompositionPoints, ParameterSamples.DefaultGranCompositionParameters);

			var actual = _normalizer.Normalize(data);

			Assert.That(actual, Is.EqualTo(expected));
		}
	}
}
