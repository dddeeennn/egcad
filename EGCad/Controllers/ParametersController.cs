using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Extensions;
using EGCad.Common.Model.Data;

namespace EGCad.Controllers
{
	public class ParametersController : InputBaseController
	{
		// GET: Parameters
		public ActionResult Index()
		{
			return View();
		}

		public JsonResult GetState()
		{
			return Data(0, new { items = Parameters.ToArray() });
		}

		public JsonResult Add(string name, string unit)
		{
			var parameters = Parameters;
			parameters.Add(new Parameter(Parameters.Count, name, unit));
			Parameters = parameters;
			return GetState();
		}

		public JsonResult Swap(int[] ids)
		{
			var parameters = Parameters;

			for (var i = 0; i < ids.Length; i++)
			{
				if (ids[i] == parameters[i].Id) continue;

				var secondIndex = parameters.FindIndex(x => x.Id == ids[i]);
				parameters.Swap(i, secondIndex);
			}

			Parameters = parameters;
			return GetState();
		}

		public JsonResult Save(string name, string unit, int id)
		{
			var parameters = Parameters;

			var paramToEdit = Parameters.First(p => p.Id == id);
			paramToEdit.Name = name;
			paramToEdit.Unit = unit;

			Parameters = parameters;

			return GetState();
		}

		public JsonResult Remove(int id)
		{
			var param = Parameters;

			param.Remove(param.FirstOrDefault(x => x.Id == id));
			param.ForEach(x => x.Id = param.IndexOf(x));

			Parameters = param;

			return GetState();
		}
	}
}