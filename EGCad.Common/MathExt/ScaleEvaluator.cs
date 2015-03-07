using System;
using System.Drawing;

namespace EGCad.Common.MathExt
{
	public class ScaleEvaluator
	{
		private readonly int _minWidth;
		private readonly int _minHeight;
		private readonly int _maxWidth;
		private readonly int _maxHeight;

		public ScaleEvaluator(int minWidth, int minHeight, int maxWidth, int maxHeight)
		{
			_minWidth = minWidth;
			_minHeight = minHeight;
			_maxWidth = maxWidth;
			_maxHeight = maxHeight;
		}

		public const double Precision = 0.1;

		/// <summary>
		/// Validates specified img size.
		/// </summary>
		/// <param name="imgSize">Size of img.</param>
		/// <returns></returns>
		public bool Validate(Size imgSize)
		{
			return imgSize.Width > _minWidth &&
				imgSize.Width < _maxWidth &&
				imgSize.Height > _minHeight &&
				imgSize.Height < _maxHeight;
		}

		public Size GetScaledSize(Size sourceSize, double k, int offset)
		{
			var width = (int)(sourceSize.Width * k) + offset;
			var height = (int)(sourceSize.Height * k) + offset;

			return new Size(width, height);
		}

		/// <summary>
		/// Gets the scale koef.
		/// return 0 if cant get scale koef
		/// </summary>
		/// <param name="imgSize">Size of  img.</param>
		/// <returns></returns>
		public double GetScaleKoef(Size imgSize)
		{
			double k = 0;

			if (imgSize.Width > _maxWidth && imgSize.Height > _maxHeight)
			{
				var widthK = ComputeScaleKoef(imgSize.Width, _maxWidth);
				var heightK = ComputeScaleKoef(imgSize.Height, _maxHeight);

				if (widthK < Precision || heightK < Precision) return 0;

				k = Math.Min(widthK, heightK);

				return Validate(GetScaledSize(imgSize, k, -2)) ? k : 0;
			}

			if (imgSize.Width < _minWidth && imgSize.Height < _minHeight)
			{
				var widthK = ComputeScaleKoef(imgSize.Width, _minWidth);
				var heightK = ComputeScaleKoef(imgSize.Height, _minHeight);

                if (widthK > Precision*100 || heightK > Precision*100) return 0;

				k = Math.Max(widthK, heightK);

				return Validate(GetScaledSize(imgSize, k, 2)) ? k : 0;
			}

			if (imgSize.Width < _minWidth || imgSize.Width > _maxWidth)
			{
				k = ComputeScaleKoef(imgSize.Width, _minWidth);
			}

			if (imgSize.Height < _minHeight || imgSize.Height > _maxHeight)
			{
				k = ComputeScaleKoef(imgSize.Height, _minHeight);
			}

			return Validate(GetScaledSize(imgSize, k, 2)) ? k : 0;
		}

		/// <summary>
		/// Compute scale koef.
		/// </summary>
		/// <param name="sideLength">Length of side.</param>
		/// <param name="normalLength">desirable side length.</param>
		/// <returns></returns>
		private double ComputeScaleKoef(double sideLength, double normalLength)
		{
			return normalLength / sideLength;
		}
	}
}
