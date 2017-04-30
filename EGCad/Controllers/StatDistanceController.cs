using System.Web.Mvc;

namespace EGCad.Controllers
{
    public class StatDistanceController : Controller
    {
        // GET: StatDistance
        public ActionResult Index()
        {
            return View(TempData["statDistanceData"]);
        }
    }
}