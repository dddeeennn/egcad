using System.IO;
using System.Web.Mvc;
using EGCad.Core.IO;

namespace EGCad.Controllers
{
	public class SaveController : InputBaseController
	{
		// GET: Save
		public FileStreamResult Index()
		{
			Save();

			string path;
			SaveService.Save(InputModel, Server.MapPath("~/App_Data/store/"), out path);

			return File(new FileStream(path, FileMode.Open), "text/xml", Path.GetFileName(path));
		}
	}
}