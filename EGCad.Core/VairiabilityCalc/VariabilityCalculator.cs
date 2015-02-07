using System;
using System.Linq;
using EGCad.Common.Extensions;

namespace EGCad.Core.VairiabilityCalc
{
	public class VariabilityCalculator
	{
		readonly EngineerGeoParameterDistributionProvider _egParameterDistributionProvider = new EngineerGeoParameterDistributionProvider();

		/// <summary>
		/// Gets the variability function.
		/// </summary>
		/// <param name="sourceData">The source data.</param>
		/// <returns>vector of variability function values. If source data is empty returns null.</returns>
		public double[] GetVariabilityFunction(Data sourceData)
		{
			if (!sourceData.Points.Any()) return null;

			var parameterDistrFunction = _egParameterDistributionProvider.Get(sourceData);

			var variabilityDataTable = GetVariabilityDataTable(parameterDistrFunction);

			var result = GenerizeFunction(variabilityDataTable);

			return result;
		}

		private double[,] GetVariabilityDataTable(double[,] sourceData)
		{
			var rowLength = sourceData.GetLength(0);
			var columnLength = sourceData.GetLength(1);

			var result = new double[rowLength, columnLength];

			for (var i = 0; i < rowLength; i++)
			{
				for (var j = 0; j < columnLength; j++)
				{
					result[i, j] = i != 0 ? result[i - 1, j] + Math.Abs(sourceData[i , j] - sourceData[i - 1, j]) : 0;
				}
			}
			return result;
		}

		private double[] GenerizeFunction(double[,] variabilityDataTable)
		{
			var result = new double[variabilityDataTable.GetLength(0)];

			for (var i = 0; i < result.Length; i++)
			{
				result[i] = variabilityDataTable.GetRow(i).Sum();
			}

			return result;
		}
	}
}
