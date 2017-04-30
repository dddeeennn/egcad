using System;
using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;

namespace EGCad.Common.Extensions
{
    public static class DataExtensions
    {
        public static double[][] TableData(this Data sourceData)
        {
            var paramCount = sourceData.Points[0].Parameters.Count;

            var sourceColumns = new Double[paramCount][];

            for (var i = 0; i < paramCount; i++)
            {
                sourceColumns[i] = sourceData.Points.SelectMany(po => po.Parameters)
                    .Where(param => param.Id == sourceData.Points[0].Parameters[i].Id)
                    .Select(par => par.Value).ToArray();
            }
            return sourceColumns;
        }

        public static double[,] ValueArray(this Data sourceData, int rowLength, int columnLength)
        {
            var result = new double[columnLength, rowLength];

            for (var i = 0; i < columnLength; i++)
            {
                for (var j = 0; j < rowLength; j++)
                {
                    result[i, j] = sourceData.Points[j].Parameters[i].Value;
                }
            }

            return result;
        }

        public static ClusterNode Get(this IEnumerable<ClusterNode> clusters, int pointId)
        {
            return clusters.First(c => c.JoinedClasters.Contains(pointId));
        }

        public static double[] RowValues(this NormalizeData data, int[] rowIdx)
        {
            return data.Rows.First(x => x.RowIdx.ToStr() == rowIdx.ToStr()).Values;
        }

        public static int[] RowIdx(this NormalizeData data, int[] rowIdx)
        {
            return data.Rows.First(x => x.RowIdx.ToStr() == rowIdx.ToStr()).RowIdx;
        }

    }
}
