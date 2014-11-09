using System.Web.Mvc;
using EGCad.Models.InputData;

namespace EGCad.Controllers
{
	public class ParametersController : InputBaseController
	{
		// GET: Parameters
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult Save(string name, string unit)
		{
			var param = Parameters;
			param.Add(new Parameter(Parameters.Count, name, unit));
			Parameters = param;
			return Data(0, new { items = Parameters.ToArray() });
		}
	}
}