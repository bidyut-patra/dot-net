using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PES.SignalR.Core;

namespace PES.SignalR.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Configures service like signalr service, DI etc.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_0);
            // Backplane used for load balancing
            services.AddSignalR();
            // run as background service
            services.AddHostedService<SignalRServiceBase>();
        }

        /// <summary>
        /// Configures the URLs
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseCors();
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotificationHub>(HubUrlRegistry.RegisterHub(typeof(NotificationHub)));
            });
        }
    }
}
