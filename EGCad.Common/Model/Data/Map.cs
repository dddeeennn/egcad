using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EGCad.Common.Model.Data
{
	/// <summary>
	/// input map 
	/// </summary>
	[Serializable]
	public class Map
	{
		private const double Tolerance = 0.000000000000001;

		public double K
		{
			get
			{
				if (Start.IsEmpty || End.IsEmpty) return 0;
				return (double)(End.Y - Start.Y) / (End.X - Start.X);
			}
		}

		public double B
		{
			get
			{
				if (Start.IsEmpty || End.IsEmpty) return 0;
				return (double)(End.X * Start.Y - End.Y * Start.X) / (End.X - Start.X);
			}
		}

		//for real-world map
		public double BT
		{
			get { return B * ScaleKoef; }
		}

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

		[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
		[ScriptIgnore]
		[XmlElement("Image")]
		public byte[] ImageSerialized
		{
			get
			{ // serialize
				if (Image == null) return null;
				using (var ms = new MemoryStream())
				{
					Image.Save(ms, ImageFormat.Bmp);
					return ms.ToArray();
				}
			}
			set
			{ // deserialize
				if (value == null)
				{
					Image = null;
				}
				else
				{
					using (var ms = new MemoryStream(value))
					{
						Image = new Bitmap(ms);
					}
				}
			}
		}

		[JsonIgnore]
		[XmlIgnore]
		public Image Image { get; set; }

		public string ImgSrc { get; set; }

		public int Width
		{
			get { return Image != null ? Image.Width : 0; }
		}

		public int Height
		{
			get { return Image != null ? Image.Height : 0; }
		}

		//canvas coord
		public Point Start { get; set; }
		public Point End { get; set; }

		//topology from real map
		public Point StartT { get; set; }
		public Point EndT { get; set; }

		public double WidthT
		{
			get
			{
				if (Start.IsEmpty || End.IsEmpty || EndT.IsEmpty) return 0;
				return Width * Math.Abs(StartT.X - EndT.X) / (double)Math.Abs(Start.X - End.X);
			}
		}

		public double HeightT
		{
			get
			{
				if (Math.Abs(AspectRatio) < Tolerance || Math.Abs(WidthT) < Tolerance) return 0;
				return WidthT / AspectRatio;
			}
		}

		public double AspectRatio
		{
			get
			{
				if (Width == 0 || Height == 0) return 0;
				return (double)Width / Height;
			}
		}

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

		public bool IsValid
		{
			get { return Image != null && !Start.IsEmpty && !End.IsEmpty && !EndT.IsEmpty; }
		}

		public double GetScaleKoef()
		{
			if (Start.IsEmpty || End.IsEmpty || EndT.IsEmpty) return 0;

			double m = StartT.X - EndT.X;
			double px = Math.Sqrt(Math.Pow((Start.X - End.X), 2) + Math.Pow((Start.Y - End.Y), 2));

			return Math.Abs(m / px);
		}
	}
}