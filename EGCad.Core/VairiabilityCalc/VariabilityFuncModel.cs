namespace EGCad.Core.VairiabilityCalc
{
	public class VariabilityFuncModel
	{
        public VariabilityFuncItem[] NewValues { get; private set; }

		public VariabilityFuncItem[] Values { get; private set; }

		public VariabilityFuncModel(VariabilityFuncItem[] old,VariabilityFuncItem[] calculated)
		{
			Values = old;
		    NewValues = calculated;
		}

		public VariabilityFuncModel()
		{
			Values = new VariabilityFuncItem[0];
		    NewValues = new VariabilityFuncItem[0];
		}
	}
}
