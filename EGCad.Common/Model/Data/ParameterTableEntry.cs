using System;
using System.Collections.Generic;

namespace EGCad.Common.Model.Data
{
    [Serializable]
    public class ParameterTableEntry
    {
        public static int Counter;

        public IList<Parameter> Parameters { get; set; }

        public int Id { get; set; }

        public double X { get; set; }

        public ParameterTableEntry(int id, double x,  IList<Parameter> parameters)
        {
            Id = id;
            X = x;
            Parameters = parameters;
        }

        public ParameterTableEntry()
        {
            Parameters = new List<Parameter>();
        }
    }
}