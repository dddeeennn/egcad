using System.Collections.Generic;
using EGCad.Core.Input;

namespace EGCad.Models
{
    public class ResultModel
    {
        public Map Map { get; set; }

        public IEnumerable<ParameterTableEntry> Points { get; set; }

        public IEnumerable<ParameterTableEntry> NewPoints { get; set; }

        public ResultModel()
        {
            Points = new ParameterTableEntry[0];
            Map = new Map();
        }

        public ResultModel(Map map, IEnumerable<ParameterTableEntry> points, IEnumerable<ParameterTableEntry> newPoints)
        {
            Map = map;
            Points = points;
            NewPoints = newPoints;
        }
    }
}