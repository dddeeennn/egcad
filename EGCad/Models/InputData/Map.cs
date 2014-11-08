using System.Drawing;

namespace EGCad.Models.InputData
{
	/// <summary>
	/// input map 
	/// </summary>
	public class Map
	{
		public Map(Image image,string src, Point start, Point end)
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

		//topology
		public Point StartT { get; set; }
		public Point EndT { get; set; }

		public double GetScaleKoef()
		{
			return (Start.X - End.X) / (StartT.X - EndT.X);
		}
	}
}