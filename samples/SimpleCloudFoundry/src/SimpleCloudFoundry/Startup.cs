﻿
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SteelToe.Extensions.Configuration.CloudFoundry;
using Pivotal.Extensions.Configuration;
using SimpleCloudFoundry.Model;
using System;

namespace SimpleCloudFoundry
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, ILoggerFactory logFactory)
        {
            logFactory.AddConsole(minLevel: LogLevel.Debug);

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()

                // Adds the Spring Cloud Configuration Server as a configuration source.
                // The settings used in contacting the Server will be picked up from
                // appsettings.json, and then overriden from any environment variables, and then
                // overriden from the CloudFoundry environment variable settings. 
                // Defaults will be used for any settings not present in any of the earlier added 
                // sources.  See ConfigServerClientSettings for defaults.
                .AddConfigServer(env, logFactory);

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Optional: Adds IConfigurationRoot as a service and also configures  IOption<ConfigServerClientSettingsOptions>,
            // IOption<CloudFoundryApplicationOptions>, IOption<CloudFoundryServicesOptions>
            // Performs:
            //      services.AddOptions();
            //      services.Configure<ConfigServerClientSettingsOptions>(config);
            //      services.Configure<CloudFoundryApplicationOptions>(config);
            //      services.Configure<CloudFoundryServicesOptions>(config);
            //      services.AddInstance<IConfigurationRoot>(config);
            services.AddConfigServer(Configuration);

            // Add framework services.
            services.AddMvc();

            // Add the configuration data returned from the Spring Cloud Config Server
            services.Configure<ConfigServerData>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
