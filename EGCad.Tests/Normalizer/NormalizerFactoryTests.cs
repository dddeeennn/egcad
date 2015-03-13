using System;
using EGCad.Core.Normalize;
using NUnit.Framework;

namespace EGCad.Tests.Normalizer
{
    [TestFixture]
    public class NormalizerFactoryTests
    {
        [Test]
        public void NormalizerFactoryForEucleadSettingsShouldReturnCorrectlyNormalizer()
        {
            var expectedType = typeof(EukleadAveragedNormalizer);

            var actual = NormalizerFactory.Create(Sample.CalculationSettingsSamples.EucleadExtendedCalculationSettings);

            Assert.That(actual, Is.TypeOf(expectedType));
        }

        [Test]
        public void NormalizerFactoryForModularSettingsShouldReturnCorrectlyNormalizer()
        {
            var expectedType = typeof(ModularNormalizer);

            var actual = NormalizerFactory.Create(Sample.CalculationSettingsSamples.ModularCalculationSettings);

            Assert.That(actual, Is.TypeOf(expectedType));
        }

        [Test]
        public void NormalizerFactoryForModularCenteredSettingsShouldReturnCorrectlyNormalizer()
        {
            var expectedType = typeof(ModularCenteredNormalizer);

            var actual = NormalizerFactory.Create(Sample.CalculationSettingsSamples.ModularCenteredCalculationSettings);

            Assert.That(actual, Is.TypeOf(expectedType));
        }

        [Test]
        public void WhenNullSettingsShouldThrowException()
        {
            Assert.Throws<NullReferenceException>(() => NormalizerFactory.Create(null));
        }
    }
}
