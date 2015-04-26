namespace EGCad.Models
{
    public class DrawPoint
    {
        public int X { get; private set; }

        public int Y { get; private set; }

        public string Title { get; private set; }

        public bool IsNew { get; private set; }

        public int ClusterIndex { get; private set; }

        public DrawPoint(int x, int y, string title, int clusterIndex, bool isNew)
        {
            X = x;
            Y = y;
            IsNew = isNew;
            Title = title;
            ClusterIndex = IsNew ? 255:clusterIndex;
        }
    }
}