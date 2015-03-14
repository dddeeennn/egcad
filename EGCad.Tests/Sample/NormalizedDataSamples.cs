using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EGCad.Common.Model.Normalize;

namespace EGCad.Tests.Sample
{
	static class NormalizedDataSamples
	{
		/// <summary>
		/// Gets gran composition normalized data (domain-specific).
		/// </summary>
		/// <value>
		/// The gran composition normalized data (domain-specific).
		/// </value>
		public static NormalizeData GranCompositionNormalizedData
		{
			get
			{
				return new NormalizeData(null);
			}
		}

		public static NormalizeData DefaultNormalizedData
		{
			get
			{
				return new NormalizeData(new []
				{
					GetNormalizeRow(),
					
				});
			}
		}

		private static NormalizeDataRow GetNormalizeRow()
		{
			return new NormalizeDataRow(null,null);
		}
	}
}
