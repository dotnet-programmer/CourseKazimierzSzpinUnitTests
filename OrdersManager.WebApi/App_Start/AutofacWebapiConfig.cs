using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NLog;
using OrdersManager.Core;
using OrdersManager.Core.Repositories;
using OrdersManager.Core.Services;
using OrdersManager.Models;
using OrdersManager.Persistence;
using OrdersManager.Persistence.Repositories;
using OrdersManager.Persistence.Services;

namespace OrdersManager.App_Start
{
	public class AutofacWebapiConfig
	{
		public static IContainer Container;

		public static void Initialize(HttpConfiguration config)
			=> Initialize(config, RegisterServices(new ContainerBuilder()));

		public static void Initialize(HttpConfiguration config, IContainer container)
			=> config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

		private static IContainer RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterType<ApplicationDbContext>()
				.As<IApplicationDbContext>()
				.InstancePerRequest();

			builder.RegisterType<UserRepository>()
				.As<IUserRepository>()
				.InstancePerRequest();

			builder.RegisterType<OrderRepository>()
				.As<IOrderRepository>()
				.InstancePerRequest();

			builder.RegisterType<UnitOfWork>()
				.As<IUnitOfWork>()
				.InstancePerRequest();

			builder.RegisterType<DiscountService>()
				.As<IDiscountService>()
				.InstancePerRequest();

			builder.RegisterType<OrderService>()
				.As<IOrderService>()
				.InstancePerRequest();

			builder.RegisterInstance<Logger>(LogManager.GetLogger("f"))
				.As<ILogger>()
				.SingleInstance();

			Container = builder.Build();

			return Container;
		}
	}
}