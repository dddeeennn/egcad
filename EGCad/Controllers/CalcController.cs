using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Infrastructure;
using EGCad.Core;
using EGCad.Core.Clasterize;
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

        public ActionResult DisplayClusterizedData()
        {
            var normalizer = new EukleadAveragedNormalizer();
            var clusterizer = new ClusterCalculator(StatCalculationType.Euclead);
            var data = clusterizer.Clusterize(normalizer.Normalize(new Data(InputModel.Points.Take(23).ToList())));
            TempData["clusterizedData"] = data;
            return RedirectToAction("Index", "Clusterize");

        }
    }
}