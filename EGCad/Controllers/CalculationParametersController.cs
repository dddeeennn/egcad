using System.Web.Mvc;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
    public class CalculationParametersController : InputBaseController
    {
        // GET: CalculationParameters
        public ActionResult Index()
        {
            return View(new CalculationParameter());
        }

        [HttpPost]
        public ActionResult Index(CalculationParameter model)
        {
            AdditionalPointCount = model.AdditionalPointCount;
            Normilize = model.Normilize;
            StatCalculation = model.StatCalculation;
	        ClusterCount = model.ClusterCount;
            return RedirectToAction("Index", "Calc");
        }
    }
}