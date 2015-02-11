using System;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.VariabilityFunction;

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
        public VariabilityFuncItem[] GetVariabilityFunction(Data sourceData)
        {
            if (!sourceData.Points.Any()) return new VariabilityFuncItem[0];

            var parameterDistrFunction = _egParameterDistributionProvider.Get(sourceData);

            var variabilityDataTable = GetVariabilityDataTable(parameterDistrFunction);

            var variabilityValues = GenerizeFunction(variabilityDataTable);

            return variabilityValues.Select(val => val / variabilityValues.Max()).ToArray()
                                    .Select((val, idx) =>
                        new VariabilityFuncItem(sourceData.Points[0].X, sourceData.Points[0].Y,
                        sourceData.Points[idx].X, sourceData.Points[idx].Y, sourceData.Points[idx].Id, val)).ToArray();
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
                    result[i, j] = i != 0 ? result[i - 1, j] + Math.Abs(sourceData[i, j] - sourceData[i - 1, j]) : 0;
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
