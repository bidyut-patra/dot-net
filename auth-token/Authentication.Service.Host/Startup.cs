using System;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(Authentication.Service.Host.Startup))]

namespace Authentication.Service.Host
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configure the app
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            Configure(config);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        /// <summary>
        /// Configures the http
        /// </summary>
        /// <param name="config"></param>
        public static void Configure(HttpConfiguration config)
        {
            SwaggerConfig.Register(config);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MessageHandlers.Add(new LogRequestHandler());

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<AuthenticationTokenService>().As<IAuthenticationService>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
