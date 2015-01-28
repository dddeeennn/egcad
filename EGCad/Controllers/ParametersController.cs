using System.Linq;
using System.Web.Mvc;
using EGCad.Common.Extensions;
using EGCad.Core.InputData;

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

        public JsonResult Replace(bool direction, int id)
        {
            var parameters = Parameters;
            parameters.Swap(id, direction ? id-1 : id+1);
            parameters.ForEach(p=>p.Id=parameters.IndexOf(p));
            Parameters = parameters;
            return GetState();
        }

        public JsonResult Save(string name, string unit, int? id)
        {
            if (id.HasValue)
            {
                var param = Parameters[id.Value];
                param.Name = name;
                param.Unit = unit;
            }
            else
            {
                var param = Parameters;
                param.Add(new Parameter(Parameters.Count, name, unit));
                Parameters = param;
            }
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