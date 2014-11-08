using System.Web.Mvc;

namespace EGCad.Controllers
{
	public class ParametersController : InputBaseController
    {
        // GET: Parameters
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(string name, string unit)
        {
            return Json(new { name = name, unit = unit }, JsonRequestBehavior.AllowGet);
        }
    }
}