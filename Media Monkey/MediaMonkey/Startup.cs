using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MediaMonkey.DataAccess.Models;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MediaMonkey
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
            // Deserializes the AppConfig section and injects the resulting object - making it available to the rest of our application.
            services.Configure<AppConfig>(Configuration.GetSection("AppConfig"));

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            #if DEBUG
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            #else
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            #endif

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options => {
                    options.LoginPath = "/sign-in";
                    options.LogoutPath = "/sign-out";
                    options.AccessDeniedPath = "/access-denied";
                    options.Cookie.Name = "UserAuth";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Check for if the project is using development (debug)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Points to TestContent folder in the project when Content path is referenced
                app.UseStaticFiles(new StaticFileOptions   
                {  
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "TestContent")),  
                        RequestPath = "/Content"  
                });
            }
            else
            {
                // TODO: Add error view!
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(); // wwwroot folder
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
