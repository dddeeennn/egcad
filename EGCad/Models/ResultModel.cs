using EGCad.Common.Model.Data;
using EGCad.Core;

namespace EGCad.Models
{
    public class ResultModel
    {
        public Map Map { get; set; }

        public EGNetwork Network { get; set; }

        public ResultModel(Map map, EGNetwork network)
        {
            Map = map;
            Network = network;
        }
    }
}