using System.Collections.Generic;
using EGCad.Core.InputData;

namespace EGCad.Core
{
    public class Data
    {
        public List<ParameterTableEntry> Points { get; private set; }

        public Data(List<ParameterTableEntry> sourcePoints)
        {
            Points = sourcePoints;
        }
    }
}
