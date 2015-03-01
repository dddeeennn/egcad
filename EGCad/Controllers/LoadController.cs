using System.Web;
using System.Web.Mvc;
using EGCad.Core.IO;

namespace EGCad.Controllers
{
	public class LoadController : InputBaseController
	{
		[HttpPost]
		public JsonResult Index(HttpPostedFileBase file)
		{
			InputModel = LoadService.Load(file.InputStream);
			
			Load();

			return Json(new {statusCode = 0});
		}
	}
}