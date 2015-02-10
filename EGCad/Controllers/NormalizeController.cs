using System.Web.Mvc;
using EGCad.Common.Model.Normalize;

namespace EGCad.Controllers
{
    public class NormalizeController : Controller
    {
        // GET: Normalize
        public ActionResult Index()
        {
            return View(TempData["normalizedData"] ?? new NormalizeData());
        }
    }
}