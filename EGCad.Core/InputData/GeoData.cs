using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Core.InputData
{
	public class GeoData
	{
		public Map Map { get; set; }

        public List<Parameter> Parameters { get; set; }

        public List<ParameterTableEntry> Points { get; set; } 

        public int AdditionalPointCount { get; set; }

        public NormalizeType Normilize { get; set; }

        public StatCalculationType StatCalculation { get; set; }

		public GeoData(Map map)
			: this()
		{
			Map = map;
		}

		public GeoData(Map map, int additionalPointCount, List<ParameterTableEntry> points ,List<Parameter> parameters, NormalizeType normalize,
			StatCalculationType statCalculation)
			: this(map)
		{
		    Points = points;
			AdditionalPointCount = additionalPointCount;
			Parameters = parameters;
			Normilize = normalize;
			StatCalculation = statCalculation;
		}

		public GeoData()
		{
			Parameters = new List<Parameter>();
			Normilize = NormalizeType.None;
			StatCalculation = StatCalculationType.None;
		}

		public bool IsValid
		{
			get
			{
				return !(Map == null ||
						!Parameters.Any() ||
						StatCalculation == StatCalculationType.None ||
						Normilize == NormalizeType.None);
			}
		}
	}
}