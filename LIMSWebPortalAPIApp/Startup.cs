using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using LIMSWebPortalAPIApp.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using LIMSWebPortalAPIApp.Contracts;
using LIMSWebPortalAPIApp.Services;
using AutoMapper;
using LIMSWebPortalAPIApp.Data.Mappings;
using DataLibrary.Db;
using DataLibrary.Data;

namespace LIMSWebPortalAPIApp
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WebConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAutoMapper(typeof(Maps));
            services.AddSwaggerGen(c=> {
                c.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "Web portal API",
                        Version = "v1",
                        Description = "This is an API for LIMS web portal"});
            });
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddControllers();
            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Default"
            });
            services.AddAuthentication("Cookieauth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "LIMS.Cookie";
                config.LoginPath = "/Home/Authenticate";
            });
            services.AddSingleton<IDataAccess, SqlDb>();
            services.AddScoped<IAnalysisData, AnalysisData>();
            services.AddScoped<IProjectData, ProjectData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=> {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web portal API");
            });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
