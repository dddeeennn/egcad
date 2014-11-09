namespace EGCad.Models.InputData
{
	/// <summary>
	/// proovide input parameter
	/// </summary>
	public class Parameter
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Unit { get; set; }

		public Parameter(int id, string name, string unit)
		{
			Id = id;
			Name = name;
			Unit = unit;
		}

		public Parameter() { }
	}
}