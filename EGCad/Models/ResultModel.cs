using System;
using System.Linq;
using EGCad.Common.Model.Data;

namespace EGCad.Models
{
	public class ResultModel
	{
		public EGNetwork Network;
		public Map Map { get; set; }

		public ResultModel(Map map, EGNetwork network)
		{
			Network = network;
			Map = map;
		}

		public DrawPoint[] Points
		{
			get { return GetPoints(); }
		}

		private DrawPoint[] GetPoints()
		{
			return Network.Points.Select(p => GetDrawPoint(p.Parameters.X, p.IsNew)).ToArray();
		}

		/// <summary>
		/// get point for draw at the canvas
		/// </summary>
		/// <param name="x">r in the polar coord system</param>
		/// <param name="isNew">new positioned point</param>
		/// <returns></returns>
		private DrawPoint GetDrawPoint(double x, bool isNew)
		{
			if (Map.End.X < Map.Start.X)
			{
				if (Map.End.Y < Map.Start.Y)
				{
					var px = (int) (Map.Start.X - x/Map.ScaleKoef*Math.Cos(Math.Atan(Map.K)));
					var py = (int) (Map.Start.Y - x/Map.ScaleKoef*Math.Sin(Math.Atan(Map.K)));
					return new DrawPoint(px, py, GetTitle(x), isNew);
				}
				else
				{
					var px = (int)(Map.Start.X - x / Map.ScaleKoef * Math.Cos(Math.Atan(Map.K)));
					var py = (int)(Map.Start.Y - x / Map.ScaleKoef * Math.Sin(Math.Atan(Map.K)));
					return new DrawPoint(px, py, GetTitle(x), isNew);
				}

			}
			if (Map.End.Y < Map.Start.Y)
			{
				var px = (int)(Map.Start.X + x / Map.ScaleKoef * Math.Cos(Math.Atan(Map.K)));
				var py = (int)(Map.Start.Y + x / Map.ScaleKoef * Math.Sin(Math.Atan(Map.K)));
				return new DrawPoint(px, py, GetTitle(x), isNew);
			}
			else
			{
				var px = (int)(Map.Start.X + x / Map.ScaleKoef * Math.Cos(Math.Atan(Map.K)));
				var py = (int)(Map.Start.Y + x / Map.ScaleKoef * Math.Sin(Math.Atan(Map.K)));
				return new DrawPoint(px, py, GetTitle(x), isNew);
			}
		}

		private string GetTitle(double x)
		{
			return string.Format("[{0} , 0]", x);
		}
	}
}