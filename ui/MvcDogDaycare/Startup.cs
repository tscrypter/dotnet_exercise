using System;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MvcDogDaycare.Services;
using Steeltoe.Common.Http.Discovery;
using Steeltoe.Connector.PostgreSql.EFCore;
using Steeltoe.Discovery.Client;
using Steeltoe.Management.Endpoint.Env;
using Steeltoe.Management.Endpoint.Health;
using Steeltoe.Management.Endpoint.Hypermedia;
using Steeltoe.Management.Endpoint.Info;
using Steeltoe.Management.Endpoint.Loggers;
using Steeltoe.Management.Endpoint.Metrics;
using ui.MvcDogDaycare.Data;

namespace MvcDogDaycare
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            // Add actuators
            services.AddHealthActuator();
            services.AddHypermediaActuator();
            services.AddLoggersActuator();
            services.AddMetricsActuator();
            services.AddInfoActuator();
            services.AddEnvActuator();

            if (!Configuration.GetValue<bool>("DisableServiceDiscovery"))
            {
                services.AddDiscoveryClient();
                services.AddHttpClient<IFacility, FacilityService>()
                    .AddHttpMessageHandler<DiscoveryHttpMessageHandler>();
            }
            else
            {
                services.AddHttpClient<IFacility, FacilityService>();
            }

            services.AddDbContext<DogDaycareContext>(options =>
                options.UseNpgsql(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Reservations}/{action=Index}/{id?}");
            });
        }
    }
}
