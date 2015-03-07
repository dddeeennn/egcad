using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using EGCad.Common.MathExt;
using NUnit.Framework;

namespace EGCad.Tests.MathExt
{
    [TestFixture]
    public class MathExtensionTests
    {
        [Test]
        public void ShouldCalculateCorrectlyArithmeticalMean()
        {
            var numbers = new double[] { 1, 2, 3, 4, 5 };
            var actual = numbers.ArithmeticalMean();

            Assert.That(actual, Is.EqualTo(3));
        }

        [Test]
        public void ShouldReturnZeroWhenEmptyArray()
        {
            var numbers = new double[0];
            var actual = numbers.ArithmeticalMean();

            Assert.That(actual, Is.EqualTo(0));
        }
    }
}
