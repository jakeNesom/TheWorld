using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using TheWorld.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace TheWorld
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            _env = env;

            // ms class to import config info from a file
            var builder = new ConfigurationBuilder()
                    .SetBasePath(_env.ContentRootPath) // root of actual project, wwwRoot - root of web files
                    .AddJsonFile("config.json")
                    .AddEnvironmentVariables();

            _config = builder.Build(); // returns an object of interface type IConfigurationRoot
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                if (_env.IsProduction())  // you have to use the aspnetcore_environtment to set the environment for this to work
                {


                    options.Filters.Add(new RequireHttpsAttribute()); // will try and use https (encryption)
                }
            })
                .AddJsonOptions(config =>
                {
                    config.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                }); // registers the MVC services so we can later 


            // we're telling the root process of the app to store this variable to make it available
            // to other classes that need it.
            services.AddSingleton(_config);
            // in order to use services you have to instantiate them in the root of your app
            // there are different methods in the service class you can use to achieve different types of 
            // instantiation with your services   .AddTransient, AddSingleton, AddScoped

            // AddScoped - we want this service to be reused but only in a single request
            if (_env.IsEnvironment("Development") || _env.IsEnvironment("Testing") )
            {
                services.AddScoped<IMailService, DebugMailService>();
            }
            else
            {
                // implement a real MailService
            }

            services.AddIdentity<WorldUser, IdentityRole>(config =>
            {
                // use this space to configure password / identity requirements like the following:
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 8;
                
            })
            .AddEntityFrameworkStores<WorldContext>(); // tells us where we want to store the identities
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });


            services.AddDbContext<WorldContext>();  // registered entity framework as well as our context (fields)

            services.AddScoped<IWorldRepository, WorldRepository>(); // might use a  MockWorldRepository w same interface for testing

            services.AddTransient<GeoCoordsService>();

            services.AddTransient<WorldContextSeedData>(); // created every time we need it

            services.AddLogging();

            
             // call them
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            WorldContextSeedData seeder,
            ILoggerFactory factory
            )
        {
            

            if (env.IsEnvironment("Development"))
            {
                app.UseDeveloperExceptionPage();
                /* This method shows verbose details you're throwing on the
                 * viewpage in the web browser
                */
                factory.AddDebug(LogLevel.Information);
            }
            else
            {
                factory.AddDebug(LogLevel.Error);
            }



            //order of options in Configure() are order of middleware execution

            //app.UseDefaultFiles();
            /* 
             * this option tells the web server to use whatever 'default' file types 
             * are in the wwwroot folder
             * 
             * - When using MVC, we're not going to use this method because
             *   the MVC framework is going to be looking for views and building
             *   a single page dynamically
              
            */
            app.UseStaticFiles();
            /*
             Tells the webserver to serve flat files instead of say, a string with html markup in it
             */

            app.UseAuthentication();  // Where we turn 'on' identitiy


            Mapper.Initialize(config =>
            {
                // viewMode. converted to entitiy and back bidrectional conversion
                config.CreateMap<TripViewModel, Trip>().ReverseMap();
                config.CreateMap<StopViewModel, Stop>().ReverseMap();
            });

            // were going ot implement some middlewoare here ( in this case MVC)
            app.UseMvc(config => 
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
            });


            seeder.EnsureSeedData().Wait();

        }
    }
}
