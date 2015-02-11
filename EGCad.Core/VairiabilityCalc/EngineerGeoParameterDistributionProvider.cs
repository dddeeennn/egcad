using System;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Data;

namespace EGCad.Core.VairiabilityCalc
{
    public class EngineerGeoParameterDistributionProvider
    {
        public double[,] Get(Data sourceData)
        {
            var rowLength = sourceData.Points[0].Parameters.Count;
            var columnLength = sourceData.Points.Length;

            var result = new double[columnLength, rowLength];

            var sourceArray = sourceData.ValueArray(rowLength, columnLength);

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
    }
}
