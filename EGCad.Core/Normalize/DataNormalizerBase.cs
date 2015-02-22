using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.Normalize;

namespace EGCad.Core.Normalize
{
    public abstract class DataNormalizerBase : IDataNormalizer
    {
        public NormalizeType Type { get; protected set; }

        protected DataNormalizerBase(NormalizeType type)
        {
            Type = type;
        }

        public NormalizeData Normalize(Data sourceData)
        {
            var result = new List<ParameterTableEntry>();

            if (!sourceData.Points.Any()) return new NormalizeData(new NormalizeDataRow[0]);

            var dataTable = sourceData.TableData();

            foreach (var point in sourceData.Points)
            {
                var normalizedParameters = new List<Parameter>();

                for (var i = 0; i < point.Parameters.Count; i++)
                {
                    var p = point.Parameters[i];
                    var z = GetZeroLevelFactor(dataTable[i]); //pass column in method
                    var v = GetVariationRange(dataTable[i]); //pass column in method
                    p.Value = (p.Value - z) / v;
                    normalizedParameters.Add(p);
                }
                result.Add(new ParameterTableEntry(point.Id, point.X,normalizedParameters));
            }

            var rows =
               result.Select(
                   point =>
                       new NormalizeDataRow(new[] { point.Id },
                                            point.Parameters.Select(param => param.Value).ToArray()))
                   .ToArray();
            return new NormalizeData(rows);
        }

        protected abstract double GetZeroLevelFactor(double[] data);//натуральное значение нулевого уровня j-фактора 

        protected abstract double GetVariationRange(double[] data);//интервал варьирования j-фактором
    }
}
