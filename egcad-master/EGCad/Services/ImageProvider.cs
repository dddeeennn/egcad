using System.Drawing;
using EGCad.Common.Extensions;
using EGCad.Common.MathExt;

namespace EGCad.Services
{
	public class ImageProvider
	{
		private readonly ScaleEvaluator _scaleEvaluator;

		public ImageProvider()
			: this(500, 500, 2000, 1000)
		{
		}

		public ImageProvider(int minWidth, int minHeight, int maxWidth, int maxHeight)
		{
			_scaleEvaluator = new ScaleEvaluator(minWidth, minHeight, maxWidth, maxHeight);
		}

		/// <summary>
		/// Gets the correct image.
		/// </summary>
		/// <param name="rawImage">The raw image.</param>
		/// <returns></returns>
		public Image GetCorrectImage(Image rawImage)
		{
			if (_scaleEvaluator.Validate(rawImage.Size)) return rawImage;

			var k = _scaleEvaluator.GetScaleKoef(rawImage.Size);

			return k > ScaleEvaluator.Precision ? PerformResizeImage(rawImage, k) : null;
		}

		/// <summary>
		/// Performs resize image.
		/// </summary>
		/// <param name="source">The source.</param>
		/// <param name="k">The k.</param>
		/// <returns></returns>
		private Image PerformResizeImage(Image source, double k)
		{
			return source.Resize(_scaleEvaluator.GetScaledSize(source.Size, k, 0));
		}
	}
}