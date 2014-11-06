using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EGCad.Controllers
{
    public class MapController : ApiController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        //handle img
        [HttpPost]
        public ActionResult Load(HttpPostedFileBase map)
        {
            var f = map;
            return Success();
        }
    }
}