using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
	public class SaveController : InputBaseController
	{
		// GET: Save
		public ActionResult Index()
		{
			Save();

			var now = DateTime.Now;
			var path =
				string.Format(Path.Combine(Server.MapPath("~/App_Data/store/"),
				string.Format("egcad{0} {1}pm {2}.{3}.{4}.xml", now.Hour, now.Minute, now.Day, now.Month, now.Year)));

			var formatter = new XmlSerializer(typeof(GeoData));

			using (var fs = new StreamWriter(path))
			{
				formatter.Serialize(fs, InputModel);
			}
			//todo send user
			return View();
		}
	}
}