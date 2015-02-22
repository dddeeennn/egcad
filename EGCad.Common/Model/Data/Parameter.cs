using System;

namespace EGCad.Common.Model.Data
{
	/// <summary>
	/// proovide input parameter
	/// </summary>
	[Serializable]
	public class Parameter
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Unit { get; set; }

		private double _value;

		public double Value
		{
			get { return double.IsNaN(_value) ? 0 : _value; }
			set { _value = value; }
		}

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
			return Unit.Equals("") ? Name : string.Format("{0}, {1}", Name, Unit);
		}
	}
}