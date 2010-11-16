﻿using System.Web.Mvc;
using System.Web.Routing;

namespace SagePayMvc.Sample {
	using SagePayMvc.Sample.Models;
	using StructureMap;

	public class MvcApplication : System.Web.HttpApplication {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

			// Configure SturctureMap so it knows about the types in this assembly.

			ObjectFactory.Configure(cfg => {
				// These types are specific to the demo project.
				cfg.For<IProductRepository>().Use<ProductRepository>();
				cfg.For<IShoppingBasket>().Use<StoreShoppingBasket>();
				cfg.For<IOrderRepository>().Use<OrderRepository>();

				// These types are in the SagePayMvc library itself.
				// They are DI-friendly, but you don't *have* to use a container if you don't want to.
				cfg.For<ITransactionRegistrar>().Use<TransactionRegistrar>();
			});

			ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
		}
	}
}