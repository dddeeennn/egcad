using System;
using System.Collections.Generic;

namespace EGCad.Common.Model.Data
{
    [Serializable]
    public class ParameterTableEntry
    {
        public static int Counter;

        public List<Parameter> Parameters { get; set; }

        public int Id { get; set; }

        public double X { get; set; }

        public double MinX { get; set; }

        public double MaxX { get; set; }

        public ParameterTableEntry(int id, double x, double minX, double maxX, List<Parameter> parameters)
        {
            Id = id;
            MinX = minX;
            MaxX = maxX;
            X = x;
            Parameters = parameters;
        }

        public ParameterTableEntry(int id, double x, List<Parameter> parameters)
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