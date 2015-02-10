using System.Web.Mvc;
using EGCad.Common.Infrastructure;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.VariabilityFunction;
using EGCad.Core;
using EGCad.Core.Clasterize;
using EGCad.Core.Normalize;
using EGCad.Core.PointPosition;
using EGCad.Core.VairiabilityCalc;
using EGCad.Models;

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
            var data = normalizer.Normalize(new Data(InputModel.Points.ToArray()));
            TempData["normalizedData"] = data;
            return RedirectToAction("Index", "Normalize");

        }

        public ActionResult DisplayClusterizedData()
        {
            var settings = new CalculationSettings(2, NormalizeType.EuklideanAveraged, StatCalculationType.Euclead, 3);

            var normalizer = new EukleadAveragedNormalizer();
            var clusterizer = new ClusterCalculator(settings);

            var data = clusterizer.Clusterize(normalizer.Normalize(new Data(InputModel.Points.ToArray())));
            TempData["clusterizedData"] = data;
            return RedirectToAction("Index", "Clusterize");
        }

        public ActionResult DisplayVariabilityFunctionData()
        {
            var variabilityFuncCalc = new VariabilityCalculator();
            var settings = new CalculationSettings(2, NormalizeType.EuklideanAveraged, StatCalculationType.Euclead, 3);

            var pointProvider = new PointProvider(settings);

            var old = variabilityFuncCalc.GetVariabilityFunction(new Data(InputModel.Points.ToArray()));
            var newPoints = pointProvider.AllocationPoint(new Data(InputModel.Points.ToArray()));

            TempData["variabilityFuncData"] = new VariabilityFunction(old, newPoints);
            return RedirectToAction("Index", "VariabilityFunction");

        }

        public ActionResult DisplayResult()
        {
            var settings = new CalculationSettings(2, NormalizeType.EuklideanAveraged, StatCalculationType.Euclead, 3);

            var networkBuilder = new EGNetworkBuilder(settings);

            var points = networkBuilder.Calculate(new Data(InputModel.Points.ToArray()));

            TempData["resultData"] = new ResultModel(InputModel.Map, points);

            return RedirectToAction("Index", "Result");
        }

    }
}