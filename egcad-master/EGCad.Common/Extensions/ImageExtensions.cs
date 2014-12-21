using System.Drawing;

namespace EGCad.Common.Extensions
{
	public static class ImageExtensions
	{
		public static Image Resize(this Image sourceImage, Size size)
		{
			return new Bitmap(sourceImage, size);
		}
	}
}
