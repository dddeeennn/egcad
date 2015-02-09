﻿using System.Collections.Generic;

namespace EGCad.Common.Model.Data
{
    public class ParameterTableEntry
    {
        public static int Counter;

        public List<Parameter> Parameters { get; set; }

        public int Id { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public ParameterTableEntry(int id, double x, double y, List<Parameter> parameters)
        {
            Id = id;
            X = x;
            Y = y;
            Parameters = parameters;
        }

        public ParameterTableEntry()
        {
            Parameters = new List<Parameter>();
        }
    }
}