using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
            if (map == null || !map.InputStream.CanRead) return Error(2, "couldnt load image");

            var filePath = Path.Combine(Server.MapPath("~/Content/map"), Path.GetFileName(map.FileName));

            var imgProvider = new ImageProvider();
            var img = imgProvider.GetCorrectImage(Image.FromStream(map.InputStream, true, true));

            img.Save(filePath);

            return Data(0, new { width = img.Width, height = img.Height, url = "/Content/map/" + map.FileName });
        }
    }
}