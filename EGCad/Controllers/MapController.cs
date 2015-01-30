using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EGCad.Core.Input;
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

        //handle img
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

            base.Map = new Map(img, imgSrc, new Point(), new Point());

            return Data(0, new { Map });
        }

        [HttpGet]
        public ActionResult EndpointLength(double x)
        {
            base.Map.EndT = new Point((int)x, 0);
            return Data(0, new { Map });
        }

        [HttpGet]
        public ActionResult SavePoint(double x, double y, bool pointType)
        {
            var p = new Point((int)x, (int)y);
            if (pointType)
            {
                base.Map.Start = p;
            }
            else
            {
                base.Map.End = p;
            }
            return Data(0, new { Map });
        }
    }
}