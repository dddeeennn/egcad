using System;
using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Infrastructure;
using EGCad.Core;
using EGCad.Core.Clasterize;
using EGCad.Core.Input;
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
            var data = normalizer.Normalize(new Data(InputModel.Points));
            TempData["normalizedData"] = data.Points;
            return RedirectToAction("Index", "Normalize");

        }

        public ActionResult DisplayClusterizedData()
        {
            var normalizer = new EukleadAveragedNormalizer();
            var clusterizer = new ClusterCalculator(StatCalculationType.Euclead);
            var data = clusterizer.Clusterize(normalizer.Normalize(new Data(InputModel.Points.ToList())));
            TempData["clusterizedData"] = data;
            return RedirectToAction("Index", "Clusterize");
        }

        public ActionResult DisplayVariabilityFunctionData()
        {
            var variabilityFuncCalc = new VariabilityCalculator();
            var pointProvider = new PointProvider(StatCalculationType.Euclead);

            var old = variabilityFuncCalc.GetVariabilityFunction(new Data(InputModel.Points));
            var newPoints = pointProvider.AllocationPoint(new Data(InputModel.Points));

            TempData["variabilityFuncData"] = new VariabilityFuncModel(old, newPoints);
            return RedirectToAction("Index", "VariabilityFunction");

        }

        public ActionResult DisplayResult()
        {
            var pointProvider = new PointProvider(StatCalculationType.Euclead);

            var points = pointProvider.AllocationPoint(new Data(InputModel.Points)).Select(
                v => new ParameterTableEntry(v.PointId, v.X, v.Y, InputModel.Parameters)).ToArray();

            TempData["resultData"] = new ResultModel(InputModel.Map, InputModel.Points, points);

            return RedirectToAction("Index", "Result");
        }

    }
}