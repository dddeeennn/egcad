using System;
using System.Drawing;
using EGCad.Common.MathExt;
using NUnit.Framework;

namespace EGCad.Tests.MathExt
{
    [TestFixture]
    public class ScaleEvaluatorTests
    {
        private ScaleEvaluator _evaluator;

        [SetUp]
        public void SetUp()
        {
            //perform default instance of evaluator
            _evaluator = new ScaleEvaluator(100, 100, 500, 800);
        }

        [Test]
        public void ScaleEvaluatorShouldValidateValidSizeReturnTrue()
        {
            var size = new Size(150, 150);

            var actual = _evaluator.Validate(size);

            Assert.IsTrue(actual);
        }

        [Test]
        public void ScaleEvaluatorShouldValidateInvalidSizeReturnFalse()
        {
            var size = new Size(150, 1150);

            var actual = _evaluator.Validate(size);

            Assert.IsFalse(actual);
        }

        [Test]
        public void ShouldReturnScaledSizeShouldReturnCorrectScaleByKoef()
        {
            var expected = new Size(200, 400);

            var size = new Size(100, 200);

            var actual = _evaluator.GetScaledSize(size, 2, 0);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturnScaledSizeShouldReturnCorrectScaleByKoefAndOffset()
        {
            var expected = new Size(250, 450);

            var size = new Size(100, 200);

            var actual = _evaluator.GetScaledSize(size, 2, 50);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldCorrectReturnScaleKoefWhenWidthAndHeightGreaterThanMaxWidthAndHeight()
        {
            // case 1 : width and height has a proportional greater size
            var expected = 0.5;
            var img = new Size(1000, 1600);
            var actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 2: width greater than height
            expected = 0.2;
            img = new Size(2500, 1500);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 3: height greater than width
            expected = 0.2;
            img = new Size(1000, 4000);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 4: width has abnormally size
            expected = 0;
            img = new Size(Int32.MaxValue, 1000);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 5: height has abnormally size
            expected = 0;
            img = new Size(1000, Int32.MaxValue);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 6: width and height has abnormally size
            expected = 0;
            img = new Size(Int32.MaxValue, Int32.MaxValue);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturnCorrectScaleKoefWhenWidthAndHeightHaveScaleLessThanMinWifthAndHeight()
        {
            // case 1 : width and height has a proportional less size
            double expected = 2;
            var img = new Size(50, 50);
            var actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 2: width  less  than height
            expected = 5;
            img = new Size(20, 90);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 3: height less than width
            expected = 5;
            img = new Size(80, 20);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 4: width has abnormally size
            expected = 0;
            img = new Size(1, 90);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 5: height has abnormally size
            expected = 0;
            img = new Size(90, Int32.MinValue);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));

            //case 6: width and height has abnormally size
            expected = 0;
            img = new Size(1, 1);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ShouldReturnCorrectScaleWhenSizeInAvailableRange()
        {
            var img = new Size(250, 400);
            var actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnCorrectScaleWhenWidthLessThanMinWidth()
        {
            // case 1 : after scaling image has correctly height
            var img = new Size(50, 300);
            var actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(2));

            // case 2 : after scaling image has an abnormally height
            img = new Size(50, 600);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(0));
        }

        [Test]
        public void ShouldReturnCorrectScaleWhenHeightLessThanMinHeight()
        {
            // case 1 : after scaling image has correctly width
            var img = new Size(200,50);
            var actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(2));

            // case 2 : after scaling image has an abnormally width
            img = new Size(500, 50);
            actual = _evaluator.GetScaleKoef(img);
            Assert.That(actual, Is.EqualTo(0));
        }
    }
}
