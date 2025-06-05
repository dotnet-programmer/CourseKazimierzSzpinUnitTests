using System.Web.Http;

namespace OrdersManager.App_Start
{
	public class Bootstrapper
	{
		public static void Run()
			=> AutofacWebapiConfig.Initialize(GlobalConfiguration.Configuration);
	}
}