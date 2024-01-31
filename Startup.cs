using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using USERFORM.Models;
using System;
using System.Collections.Generic;

namespace USERFORM
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddSessionStateTempDataProvider();
            services.AddScoped<ModelContext>();
            services.AddScoped<ModelContext>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute("areaRoute", "{area:exists}/{controller=Account}/{action=Login}/{id?}");
            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Account}/{action=Login}/{id?}"
            //    );
            //});
            app.UseMvc(routes =>
            {
                routes.MapRoute("areaRoute", "{area:exists}/{controller=USERF01}/{action=Create}/{id?}");
                routes.MapRoute(
                    name: "createRoute",
                    template: "{area=M1}/{controller=USERF01}/{action=Create}/{id?}"
                );
            });

            // Add a new route for handling OTP generation
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "sendOTPRoute",
                    template: "{area=M1}/{controller=USERF01}/{action=SendOTP}/{id?}"
                );
            });
        }
    }
}
