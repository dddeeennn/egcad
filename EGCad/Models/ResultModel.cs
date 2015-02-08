using EGCad.Core.Input;

namespace EGCad.Models
{
    public class ResultModel
    {
        public Map Map { get; set; }

        public ParameterTableEntry[] Points { get; set; }

        public ResultModel()
        {
            Points = new ParameterTableEntry[0];
            Map = new Map();
        }

        public ResultModel(Map map, params ParameterTableEntry[] points)
        {
            Map = map;
            Points = points;
        }
    }
}