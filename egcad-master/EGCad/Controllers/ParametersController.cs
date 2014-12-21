using System.Linq;
using System.Web.Mvc;
using EGCad.Models.InputData;

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

        public JsonResult Save(string name, string unit)
        {
            var param = Parameters;
           
            param.Add(new Parameter(Parameters.Count, name, unit));
            Parameters = param;

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