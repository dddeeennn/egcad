namespace EGCad.Common.Model.VariabilityFunction
{
	public class VariabilityFunction
	{
        public VariabilityFuncItem[] NewValues { get; private set; }

		public VariabilityFuncItem[] Values { get; private set; }

		public VariabilityFunction(VariabilityFuncItem[] old,VariabilityFuncItem[] calculated)
		{
			Values = old;
		    NewValues = calculated;
		}

		public VariabilityFunction()
		{
			Values = new VariabilityFuncItem[0];
		    NewValues = new VariabilityFuncItem[0];
		}
	}
}
