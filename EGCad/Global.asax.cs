using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace EGCad
{
	public class MvcApplication : HttpApplication
	{
		private static NLog.Logger logger = LogManager.GetCurrentClassLogger();

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			logger.Error("app start");
#if DEBUG
			foreach (var bundle in BundleTable.Bundles)
			{
				bundle.Transforms.Clear();
			}
#endif
		}


		protected void Session_OnEnd(object sender, EventArgs e)
		{
			logger.Error("session is expire!");
			
		}
	}
}
