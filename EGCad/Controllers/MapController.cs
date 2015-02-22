using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EGCad.Common.Model.Data;
using EGCad.Services;

namespace EGCad.Controllers
{
    public class MapController : InputBaseController
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetState()
        {

            return Data(0, new { Map });
        }

        //handle img load
        [HttpPost]
        public ActionResult Load(HttpPostedFileBase map)
        {
            if (map == null || !map.InputStream.CanRead) return Error(2, "Не получается загрузить изображение ");

            var filePath = Path.Combine(Server.MapPath("~/Content/map"), Path.GetFileName(map.FileName));

            var img = new ImageProvider().GetCorrectImage(Image.FromStream(map.InputStream, true, true));

            if (img == null)
                return Error(2,
                    string.Format("Загрузите изображение корректного размера!Ширина {0}...{1}px, высота {2}...{3}px", 500, 2000,
                        500, 1000));

            img.Save(filePath);

            var imgSrc = "/Content/map/" + map.FileName;

            if (Map == null)
            {
                Map = new Map(img, imgSrc, new Point(), new Point());
            }
            else
            {
                Map.Image = img;
                Map.ImgSrc = imgSrc;
            }

            return GetState();
        }

        [HttpGet]
        public ActionResult EndpointLength(double x)
        {
            if (Map == null) Map = new Map();
            Map.EndT = new Point((int)x, 0);
            return GetState();
        }

        [HttpGet]
        public ActionResult SavePoint(double x, double y, bool pointType)
        {
            var p = new Point((int)x, (int)y);
            if (pointType)
            {
                Map.Start = p;
            }
            else
            {
                Map.End = p;
            }
            return GetState();
        }
    }
}