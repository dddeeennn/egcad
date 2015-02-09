using System.Collections.Generic;
using System.Linq;
using EGCad.Common.Extensions;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;

namespace EGCad.Core.Normalize
{
    public abstract class DataNormalizerBase : IDataNormalizer
    {
        public NormalizeType Type { get; protected set; }

        protected DataNormalizerBase(NormalizeType type)
        {
            Type = type;
        }

        public Data Normalize(Data sourceData)
        {
            var result = new List<ParameterTableEntry>();

            if (!sourceData.Points.Any()) return new Data(result.ToArray());

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
                result.Add(new ParameterTableEntry(point.Id, point.X, point.Y, normalizedParameters));
            }
            return new Data(result.ToArray());
        }

        protected abstract double GetZeroLevelFactor(double[] data);//натуральное значение нулевого уровня j-фактора 

        protected abstract double GetVariationRange(double[] data);//интервал варьирования j-фактором
    }
}
