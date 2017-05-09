using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using EGCad.Common.Model.Clusterize;
using EGCad.Common.Model.Data;
using EGCad.Common.Model.VariabilityFunction;
using EGCad.Core;
using EGCad.Core.Clasterize;
using EGCad.Core.Normalize;
using EGCad.Core.PointPosition;
using EGCad.Core.VairiabilityCalc;
using EGCad.Models;
using WebGrease.Css.Extensions;

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

            var clusterCalculator = new ClusterCalculator(Settings);
            var clusterizedData = clusterCalculator.Clusterize(new Data(InputModel.PointsToCalc));

            var networkBuilder = new EGNetworkBuilder(settings);

            var points = networkBuilder.Calculate(new Data(InputModel.PointsToCalc));

            TempData["resultData"] = new ResultModel(InputModel.Map, points, clusterizedData);
            return RedirectToAction("Index", "Result");
        }

        public ActionResult DownloadStatDistance()
        {
            var clusterizer = new ClusterCalculator(Settings);

            var data = clusterizer.Clusterize(new Data(InputModel.PointsToCalc));
            var builder = new StringBuilder();

            while (data.StatDistanceTables.Any())
            {
                var table = data.StatDistanceTables.Dequeue();
                builder.AppendLine();
                var tableDimentions = table.Rows[0].Cells.Count();
                builder.AppendLine(tableDimentions + " x " + tableDimentions);
                table.Rows.ForEach(x => builder.AppendLine(string.Join(";", x.Cells.Select(cell => Math.Round(cell.Value, 3)))));
            }
            builder.AppendLine();

            // builder.AppendLine("e ;" + string.Join(";", data.StatDistanceQualityKoef)); 
            builder.AppendLine("inner distance;" + string.Join(";", data.ClusterInnerDistance));
            builder.AppendLine("inner distance normalized;" + string.Join(";", data.ClusterInnerDistanceNormalized));
            builder.AppendLine("outer distance;" + string.Join(";", data.ClusterOuterDistance));
            // builder.AppendLine("cluster dispersion;" + string.Join(";", data.ClusterDispersion));

            var now = DateTime.Now;
            var filename = string.Format("statDistance{0}{1}{2}-{3}{4}{5}.csv",
            now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);

            var fileContent = builder.ToString();
            return File(Encoding.UTF8.GetBytes(fileContent), "text/csv", filename);
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