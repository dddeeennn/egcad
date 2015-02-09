using System.Collections.Generic;

namespace EGCad.Common.Model.Data
{
    // ReSharper disable once InconsistentNaming
    public class EGNetwork
    {
        public IEnumerable<EGNetworkPoint> Points { get; private set; }

        public EGNetwork(IEnumerable<EGNetworkPoint> points) : this()
        {
            Points = points;
        }

        public EGNetwork()
        {
            Points = new EGNetworkPoint[0];
        }
    }
}
