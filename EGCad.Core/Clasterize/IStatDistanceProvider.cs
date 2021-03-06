﻿using EGCad.Common.Infrastructure;

namespace EGCad.Core.Clasterize
{
    public interface IStatDistanceProvider
    {
        double GetStatDistance(double[] row1, double[] row2);
        StatCalculationType Type { get; }
    }
}
