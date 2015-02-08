using System;
using System.Drawing;

namespace EGCad.Core.Input
{
    /// <summary>
    /// input map 
    /// </summary>
    public class Map
    {
        private const double Tolerance = 0.000000000000001;

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

        public double GetScaleKoef()
        {
            if (Start.IsEmpty || End.IsEmpty || EndT.IsEmpty) return 0;

            double m = StartT.X - EndT.X;
            double px = Start.X - End.X;

            return Math.Abs(m / px);
        }
    }
}