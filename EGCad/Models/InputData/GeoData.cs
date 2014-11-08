using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Infrastructure;

namespace EGCad.Models.InputData
{
	public class GeoData
	{
		public Map Map;

		public List<Parameter> Parameters;

		public int AdditionalPointCount;

		public NormalizeType Normilize;

		public StatCalculationType StatCalculation;

		public GeoData(Map map)
			: this()
		{
			Map = map;
		}

		public GeoData(Map map, int additionalPointCount, List<Parameter> parameters, NormalizeType normalize,
			StatCalculationType statCalculation)
			: this(map)
		{
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