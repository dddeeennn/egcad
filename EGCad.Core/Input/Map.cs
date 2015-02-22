using System;
using System.Drawing;

namespace EGCad.Core.Input
{
	/// <summary>
	/// input map 
	/// </summary>
	public class Map
	{
        public Map(Image image, string src, Point start, Point end)
			: this()
		{
			ImgSrc = src;
			Image = image;
			Start = start;
			End = end;
		}

		public Map()
		{
			StartT = new Point(0, 0);
		}

		public string ImgSrc { get; set; }

		public Image Image { get; set; }

		//canvas coord
		public Point Start { get; set; }
		public Point End { get; set; }

		//topology from real map
		public Point StartT { get; set; }
		public Point EndT { get; set; }

		/// <summary>
		/// Get scale koef m/px
		/// </summary>
		/// <value>
		/// The scale koef.
		/// </value>
		public double ScaleKoef
		{
			get { return GetScaleKoef(); }
		}

		public double GetScaleKoef()
		{
			if (Start.IsEmpty || End.IsEmpty || EndT.IsEmpty) return 0;

			double m = StartT.X - EndT.X;
			double px = Start.X - End.X;

			return Math.Abs(m / px);
		}
	}
}