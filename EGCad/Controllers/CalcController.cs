using System.Web.Mvc;
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
			var normalizer = NormalizerFactory.Create(Settings);
			var data = normalizer.Normalize(new Data(InputModel.PointsToCalc));
			TempData["normalizedData"] = data;
			return RedirectToAction("Index", "Normalize");
		}

		public ActionResult DisplayClusterizedData()
		{
			var clusterizer = new ClusterCalculator(Settings);

			var data = clusterizer.Clusterize(new Data(InputModel.PointsToCalc));
			TempData["clusterizedData"] = data;
			return RedirectToAction("Index", "Clusterize");
		}

		public ActionResult DisplayVariabilityFunctionData()
		{
			var sourceData = new Data(InputModel.PointsToCalc);

			var variabilityFuncCalc = new VariabilityCalculator();

			var pointProvider = new PointProvider(Settings);

			var old = variabilityFuncCalc.GetVariabilityFunction(sourceData);
			var newPoints = pointProvider.AllocationPoint(sourceData);

			TempData["variabilityFuncData"] = new VariabilityFunction(old, newPoints);
			return RedirectToAction("Index", "VariabilityFunction");

		}

		public ActionResult DisplayResult()
		{
			// !!! work around !!! for position point between cluster
			var settings = new CalculationSettings(Settings.AdditionalPointCount, Settings.Normilize, Settings.StatCalculation,
				Settings.AdditionalPointCount + 1);

			var networkBuilder = new EGNetworkBuilder(settings);

			var points = networkBuilder.Calculate(new Data(InputModel.PointsToCalc));

			TempData["resultData"] = new ResultModel(InputModel.Map, points);
			return RedirectToAction("Index", "Result");
		}

		private CalculationSettings Settings
		{
			get
			{
				return new CalculationSettings(InputModel.AdditionalPointCount, InputModel.Normilize,
				InputModel.StatCalculation, InputModel.ClusterCount);
			}
		}
	}
}