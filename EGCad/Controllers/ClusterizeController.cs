using System.Web.Mvc;
using EGCad.Core.Clasterize;

namespace EGCad.Controllers
{
    public class ClusterizeController : Controller
    {
        // GET: Clusterize
        public ActionResult Index()
        {
            return View((ClusterNode[])TempData["clusterizedData"] ?? new ClusterNode[0]);
        }
    }
}