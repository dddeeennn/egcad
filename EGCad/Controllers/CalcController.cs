using System;
using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Infrastructure;
using EGCad.Core;
using EGCad.Core.Clasterize;
using EGCad.Core.Normalize;
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
            var data = clusterizer.Clusterize(normalizer.Normalize(new Data(InputModel.Points.Take(29).ToList())));
            TempData["clusterizedData"] = data;
            return RedirectToAction("Index", "Clusterize");
        }

        public ActionResult DisplayVariabilityFunctionData()
        {
            var variabilityFuncCalc = new VariabilityCalculator();
            var data = variabilityFuncCalc.GetVariabilityFunction(new Data(InputModel.Points));
            TempData["variabilityFuncData"] = new VariabilityFuncModel(data.Select(val => val / data.Max()).ToArray().
                Select((val, idx) =>
                    new VariabilityFuncItem(InputModel.Points[0].X, InputModel.Points[0].Y,
                        InputModel.Points[idx].X, InputModel.Points[idx].Y, val)).ToArray());

            return RedirectToAction("Index", "VariabilityFunction");

        }

        public ActionResult DisplayResult()
        {
            TempData["resultData"] = new ResultModel(InputModel.Map, InputModel.Points.ToArray());

            return RedirectToAction("Index", "Result");

        }

    }
}