using System.Web.Mvc;

namespace EGCad.Controllers
{
    public class ApiController : Controller
    {
        public JsonResult Data(int statusCode, object data)
        {
            return Json(new { statusCode, data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Data(object data)
        {
            return Data(0, data);
        }

        public JsonResult Success()
        {
            return Json(new { statusCode = 0 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Error(int statusCode)
        {
            return Json(new { statusCode });
        }

        public JsonResult Error(int statusCode, string message)
        {
            var data = new { message };
            return Json(new { statusCode, data }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Error()
        {
            return Error(1);
        }
    }
}