using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Identity;
using WebStore.Employees.DAL.Context;
using WebStore.Interfaces.Services;
using WebStore.Logger;
using WebStore.Services.Data;
using WebStore.Services.Products.InCookies;
using WebStore.Services.Products.InSqlDataBase;

namespace WebStore.ServiceHosting
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
            if (Environment.MachineName == "DESKTOP-CLR05D4")
            {
                services.AddDbContext<WebStoreDB>(x => x.UseSqlServer(Configuration.GetConnectionString("WebStoreDbConnection_DESKTOP-CLR05D4")));
                services.AddDbContext<EmployeesDb>(x => x.UseSqlServer(Configuration.GetConnectionString("EmployeesDbConnection_DESKTOP-CLR05D4")));
            }
            else if (Environment.MachineName == "DESKTOP-NFGP0QV")
            {
                services.AddDbContext<WebStoreDB>(x => x.UseSqlServer(Configuration.GetConnectionString("WebStoreDbConnection_DESKTOP-NFGP0QV")));
                services.AddDbContext<EmployeesDb>(x => x.UseSqlServer(Configuration.GetConnectionString("EmployeesDbConnection_DESKTOP-NFGP0QV")));
            }
            else
                throw new Exception("?? ??????? ???????????? ? ??????-???? ??????? ??");
            
            services.AddTransient<WebStoreDbInitializer>();
            services.AddTransient<EmployeesDbInitializer>();

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebStoreDB>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(opt => {
#if DEBUG
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;
#endif
                opt.User.RequireUniqueEmail = false;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

                opt.Lockout.AllowedForNewUsers = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            });

            services.AddTransient<IEmployeesDataService, InSqlDbEmployeesData>();      // ???????? ?????? ??? ?????? ?? ??????? ???????????
            services.AddTransient<IProductData, InSqlDbProductData>();
            services.AddScoped<ICartService, InCookiesCartService>();
            services.AddTransient<IOrderService, SqlOrderService>();

            services.AddControllers();

            const string webstore_api_xml = "WebStore.ServiceHosting.xml";
            const string webstore_domain_xml = "WebStore.Domain.xml";
            const string debug_path = "bin/Debug/net5.0/";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebStore.ServiceHosting", Version = "v1" });

                c.IncludeXmlComments(webstore_api_xml);

                if (File.Exists(webstore_domain_xml))
                    c.IncludeXmlComments(webstore_domain_xml);
                else if (File.Exists(Path.Combine(debug_path, webstore_domain_xml)))
                    c.IncludeXmlComments(Path.Combine(debug_path, webstore_domain_xml));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger)
        {
            logger.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebStore.ServiceHosting v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
