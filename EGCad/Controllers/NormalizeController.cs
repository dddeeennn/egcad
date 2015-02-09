using System.Collections.Generic;
using System.Web.Mvc;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
    public class NormalizeController : Controller
    {
        // GET: Normalize
        public ActionResult Index()
        {
            return View(TempData["normalizedData"] ?? new ParameterTableEntry[0]);
        }
    }
}