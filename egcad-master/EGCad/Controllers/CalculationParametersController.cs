using System.Web.Mvc;
using EGCad.Models;

namespace EGCad.Controllers
{
    public class CalculationParametersController : InputBaseController
    {
        // GET: CalculationParameters
        public ActionResult Index()
        {
            return View(new CalculationParameterModel());
        }

        [HttpPost]
        public ActionResult Index(CalculationParameterModel model)
        {
            AdditionalPointCount = model.AdditionalPointCount;
            Normilize = model.Normilize;
            StatCalculation = model.StatCalculation;
            return RedirectToAction("Index", "Calc");
        }
    }
}