using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FacilityService.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Common.Hosting;
using Steeltoe.Discovery.Client;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Steeltoe.Extensions.Configuration.Placeholder;
using Steeltoe.Extensions.Logging;
using Steeltoe.Management.Endpoint;

namespace FacilityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    SeedData.Initialize(services);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred seeding the DB");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((builderContext, loggingBuilder) =>
                {
                    // Add dynamic logging
                    loggingBuilder.AddDynamicConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .AddCloudFoundryConfiguration()
                .AddDiscoveryClient()
                .AddHealthActuator()
                .AddHypermediaActuator()
                .AddTraceActuator()
                .AddLoggersActuator()
                .AddMetricsActuator()
                .AddEnvActuator()
                .AddInfoActuator()
                .AddPlaceholderResolver()
                .UseCloudHosting(80);
    }
}
