using System;
using System.Linq;
using EGCad.Common.Extensions;

namespace EGCad.Core.VairiabilityCalc
{
	public class EngineerGeoParameterDistributionProvider
	{
		public double[,] Get(Data sourceData)
		{
			var rowLength = sourceData.Points[0].Parameters.Count;
			var columnLength = sourceData.Points.Count;

			var result = new double[columnLength, rowLength];

			var sourceArray = PreprocessData(sourceData, rowLength, columnLength);

			for (var i = 0; i < result.GetLength(0); i++)
			{
				for (var j = 0; j < result.GetLength(1); j++)
				{
					var columnArray = sourceArray.GetColumn(j);
					result[i, j] = (columnLength * sourceArray[i, j] - columnArray.Sum()) /
						(Math.Sqrt(columnArray.Select(value => value * value).Sum()));
				}
			}

			return result;
		}


		/// <summary>
		/// Preprocess data. Get table of values from source data
		/// </summary>
		/// <param name="sourceData">The source data.</param>
		/// <param name="rowLength"></param>
		/// <param name="columnLength"></param>
		/// <returns></returns>
		private double[,] PreprocessData(Data sourceData, int rowLength, int columnLength)
		{
			var result = new double[columnLength, rowLength];

			for (var i = 0; i < columnLength; i++)
			{
				for (var j = 0; j < rowLength; j++)
				{
					result[i, j] = sourceData.Points[i].Parameters[j].Value;
				}
			}

			return result;
		}
	}
}
