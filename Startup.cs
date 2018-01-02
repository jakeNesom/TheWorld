using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace TheWorld
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); // registers the MVC services so we can later 
            // call them
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
                /* This method shows verbose details you're throwing on the
                 * viewpage in the web browser
                */
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

            // were going ot implement some middlewoare here ( in this case MVC)
            app.UseMvc(config => 
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
            });

        }
    }
}
