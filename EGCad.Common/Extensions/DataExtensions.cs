using System;
using System.Linq;
using EGCad.Common.Model.Data;

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
    }
}
