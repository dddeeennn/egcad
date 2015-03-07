using System.Globalization;
using System.Linq;
using System.Web.Mvc;


namespace EGCad.Infrastructure
{
	public class DoubleArrayModelBinder : DefaultModelBinder
	{
		public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			if (bindingContext.ModelType != typeof(double[]))
				return base.BindModel(controllerContext, bindingContext);

			var source = bindingContext.ValueProvider.GetValue("values").RawValue as string[];

			return source != null ? source.Select(val => double.Parse(val, CultureInfo.InvariantCulture)).ToArray() : base.BindModel(controllerContext, bindingContext);
		}
	}
}