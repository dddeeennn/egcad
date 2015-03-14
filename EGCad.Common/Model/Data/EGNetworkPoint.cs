using System.Collections.Generic;
using EGCad.Common.Model.VariabilityFunction;

namespace EGCad.Common.Model.Data
{
    // ReSharper disable once InconsistentNaming
    public class EGNetworkPoint
    {
        public ParameterTableEntry Parameters { get; private set; }

        public VariabilityFuncItem VariabilityFuncValue { get; private set; }

        public bool IsNew { get; private set; }

        public EGNetworkPoint(ParameterTableEntry parameters, VariabilityFuncItem variability, bool isNew)
        {
            IsNew = isNew;
            Parameters = parameters;
            VariabilityFuncValue = variability;
        }

        public EGNetworkPoint(List<Parameter> parameters, VariabilityFuncItem variability, bool isNew) :
            this(new ParameterTableEntry(variability.PointId, variability.X, parameters), variability, isNew)
        {
        }
    }
}
