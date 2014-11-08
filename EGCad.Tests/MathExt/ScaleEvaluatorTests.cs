using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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
			_evaluator = new ScaleEvaluator(100,100,500,800);
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
	}
}
