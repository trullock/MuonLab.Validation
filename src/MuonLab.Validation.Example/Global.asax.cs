using System.Web.Mvc;
using System.Web.Routing;

namespace MuonLab.Validation.Example
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			RouteTable.Routes.MapRoute(
				"Default",
				"{controller}/{action}",
				new { controller = "Home", action = "Index" }
			);

			ModelBinders.Binders.DefaultBinder = new CustomModelBinder();
		}
	}
}