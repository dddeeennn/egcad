using System.Collections.Generic;
using System.Web.Mvc;
using EGCad.Core.InputData;

namespace EGCad.Controllers
{
    public class NormalizeController : Controller
    {
        // GET: Normalize
        public ActionResult Index()
        {
            return View((List<ParameterTableEntry>)TempData["normalizedData"] ?? new List<ParameterTableEntry>());
        }
    }
}