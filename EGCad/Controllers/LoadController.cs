using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
	public class LoadController : InputBaseController
	{
		// GET: Load
		public ActionResult Index(HttpPostedFileBase file)
		{
			var formatter = new XmlSerializer(typeof(GeoData));

			var data = formatter.Deserialize(file.InputStream);

			InputModel = (GeoData)data;

			return View();
		}
	}
}