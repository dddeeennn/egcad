using System.Web.Mvc;
using System.Web.Routing;
using EGCad.Core;
using EGCad.Core.Normalize;

namespace EGCad.Controllers
{
    public class CalcController : InputBaseController
    {
        // GET: Calc
        public ActionResult Index()
        {
            Save();
            return View(InputModel);
        }

        public ActionResult DisplayNormalizedData()
        {
            var normalizer = new EukleadAveragedNormalizer();
            var data = normalizer.Normalize(new Data(InputModel.Points));
            TempData["normalizedData"] = data.Points;
            return RedirectToAction("Index", "Normalize");

        }
    }
}