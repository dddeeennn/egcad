namespace EGCad.Core.InputData
{
    /// <summary>
    /// proovide input parameter
    /// </summary>
    public class Parameter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Value { get; set; }

        public Parameter(int id, string name, string unit)
        {
            Id = id;
            Name = name;
            Unit = unit;
        }

        public Parameter(int id, string name, string unit, double value)
            : this(id, name, unit)
        {
            Value = value;
        }

        public Parameter() { }

        public override string ToString()
        {
            return string.Format("{0}, {1}", Name, Unit);
        }
    }
}