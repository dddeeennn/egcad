using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EGCad
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);


#if DEBUG
			foreach (var bundle in BundleTable.Bundles)
			{
				bundle.Transforms.Clear();
			}
#endif
		}
	}
}
