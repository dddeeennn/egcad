using System.Web.Mvc;

namespace EGCad.Controllers
{
	public class VariabilityFunctionController : Controller
	{
		// GET: VariabilityFunction
		public ActionResult Index()
		{
			return View(TempData["variabilityFuncData"]);
		}
	}
}