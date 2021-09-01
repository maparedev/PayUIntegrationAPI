using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JckShopping.AllPaymentGateways;
using JckShopping.Data;
using JckShopping.Data.Entities;
using JckShopping.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace JckShopping
{
    public class Startup
    {
        private IConfiguration _config { get; }
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddIdentity<JKCStoreUser, IdentityRole>(cfg =>
             {
                 cfg.User.RequireUniqueEmail = true;
 
             }).AddEntityFrameworkStores<JKCContext>();


            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg=>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = _config["Token:Issuer"],
                        ValidAudience = _config["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]))
                    };
                });


            services.AddDbContext<JKCContext>(cfg =>
            {
                cfg.UseSqlServer(_config["ConnectionString:JKCContextDb"]);
            });

            services.AddTransient<JCKSeeder>();

            services.AddTransient<IBasePaymentGateway, PayUBizPaymentGateway>();

            services.AddTransient<IMailService, NullMailService>();

            services.AddScoped<IJKCRepository, JKCRepository>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddRazorPages();
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
                app.UseExceptionHandler("/error");
            }

            app.UseCors(
             options => options.WithOrigins(
              "http://localhost:4200").AllowAnyMethod().AllowAnyHeader()
                );

            app.UseDefaultFiles();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(cfg =>
            {
                cfg.MapRazorPages();

                cfg.MapControllerRoute("Default",
                    "/{controller}/{action}/{id?}",
                    new { controller = "App", action = "index" });

            });
        }
    }
}
