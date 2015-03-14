using System.Collections.Generic;

namespace EGCad.Common.Model.Data
{
    public class Data
    {
        public ParameterTableEntry[] Points { get; private set; }

        public List<Parameter> Parameters { get; private set; }

        public Data(ParameterTableEntry[] sourcePoints, List<Parameter> parameters)
        {
            Points = sourcePoints;
            Parameters = parameters;
        }

        public Data(ParameterTableEntry[] sourcePoints) : this(sourcePoints, sourcePoints[0].Parameters)
        {
        }
    }
}
