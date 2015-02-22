using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Infrastructure;
using EGCad.Common.Extensions;

namespace EGCad.Common.Model.Data
{
	public class GeoData
	{
		public Map Map { get; set; }

		public List<Parameter> Parameters { get; set; }

		public List<ParameterTableEntry> Points { get; set; }

		public ParameterTableEntry[] PointsToCalc
		{
			get { return ObjectExtensions.DeepCopy(Points.ToArray()); }
		}

		public int AdditionalPointCount { get; set; }

		public int ClusterCount { get; set; }

		public NormalizeType Normilize { get; set; }

		public StatCalculationType StatCalculation { get; set; }

		public GeoData(Map map)
			: this()
		{
			Map = map;
		}

		public GeoData(Map map, int additionalPointCount, List<ParameterTableEntry> points, List<Parameter> parameters, NormalizeType normalize,
			StatCalculationType statCalculation, int clusterCount)
			: this(map)
		{
			ClusterCount = clusterCount;
			Points = points;
			AdditionalPointCount = additionalPointCount;
			Parameters = parameters;
			Normilize = normalize;
			StatCalculation = statCalculation;
		}

		public GeoData()
		{
			Parameters = new List<Parameter>();
			Normilize = NormalizeType.EuklideanAveraged;
			StatCalculation = StatCalculationType.Euclead;
		}

		public bool IsValid
		{
			get
			{
				return !(Map == null ||
						!Parameters.Any());
			}
		}
	}
}