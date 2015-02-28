using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NLog;

namespace EGCad
{
	public class MvcApplication : HttpApplication
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();

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

        public void Application_End()
        {

            var runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime",
                                                                        BindingFlags.NonPublic
                                                                        | BindingFlags.Static
                                                                        | BindingFlags.GetField,
                                                                        null,
                                                                        null,
                                                                        null);

            if (runtime == null)
                return;

            var shutDownMessage = (string)runtime.GetType().InvokeMember("_shutDownMessage",
                                                                             BindingFlags.NonPublic
                                                                             | BindingFlags.Instance
                                                                             | BindingFlags.GetField,
                                                                             null,
                                                                             runtime,
                                                                             null);

            var shutDownStack = (string)runtime.GetType().InvokeMember("_shutDownStack",
                                                                           BindingFlags.NonPublic
                                                                           | BindingFlags.Instance
                                                                           | BindingFlags.GetField,
                                                                           null,
                                                                           runtime,
                                                                           null);

            logger.Error("\r\n\r\n_shutDownMessage={0}\r\n\r\n_shutDownStack={1}", shutDownMessage, shutDownStack);
        }

		protected void Session_OnEnd(object sender, EventArgs e)
		{
			logger.Error("session is expire!");
			
		}
	}
}
