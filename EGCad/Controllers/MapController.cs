using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using EGCad.Models.InputData;
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

		//handle img
		[HttpPost]
		public ActionResult Load(HttpPostedFileBase map)
		{
			if (map == null || !map.InputStream.CanRead) return Error(2, "couldnt load image");

			var filePath = Path.Combine(Server.MapPath("~/Content/map"), Path.GetFileName(map.FileName));

			var img = new ImageProvider().GetCorrectImage(Image.FromStream(map.InputStream, true, true));

			if (img == null)
				return Error(2,
					string.Format("Загрузите изображение корректного размера!Ширина {0}...{1}px, высота {2}...{3}px", 500, 2000,
						500, 1000));

			img.Save(filePath);

			base.Map = new Map(img, new Point(), new Point());

			return Data(0, new { width = img.Width, height = img.Height, url = "/Content/map/" + map.FileName });
		}

		[HttpGet]
		public ActionResult EndpointLength(int x)
		{
			base.Map.EndT = new Point(x, 0);
			return Success();
		}

		[HttpGet]
		public ActionResult SavePoint(int x, int y, bool pointType)
		{
			if (pointType)
			{
				base.Map.Start = new Point(x, y);
			}
			else
			{
				base.Map.End = new Point(x, y);
			}
			return Success();
		}
	}
}