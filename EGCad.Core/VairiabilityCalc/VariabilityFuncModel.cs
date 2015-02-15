namespace EGCad.Core.VairiabilityCalc
{
	public class VariabilityFuncModel
	{
		public VariabilityFuncItem[] Values { get; private set; }

		public VariabilityFuncModel(params VariabilityFuncItem[] items)
		{
			Values = items;
		}

		public VariabilityFuncModel()
		{
			Values = new VariabilityFuncItem[0];
		}
	}
}
