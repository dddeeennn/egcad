using System.Collections.Generic;

namespace EGCad.Models.InputData
{
    public class ParameterTableEntry
    {
        public List<Parameter> Parameters { get; set; }

        public int Id { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public ParameterTableEntry(int id, int x, int y, List<Parameter> parameters )
        {
            
        }

        public ParameterTableEntry()
        {
            Parameters = new List<Parameter>();
        }
    }
}