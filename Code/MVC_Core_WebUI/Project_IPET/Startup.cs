using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_IPET.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Project_IPET
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
            // 啟用 Session
            services.AddSession();
            services.AddControllersWithViews();
            //DI 依賴注入 (Dependency Injection) 
            //卉榆使用Dapper的連線方式到DB，其他人使用的Entity Framework連線字串請放在這串下方，謝謝。
            services.AddScoped<IDbConnection, SqlConnection>(serviceProvider => {
                SqlConnection conn = new SqlConnection();
                //指派連線字串
                conn.ConnectionString = Configuration.GetConnectionString("MyProjectDbConnectionString");
                return conn;
            });
            services.AddScoped<IProductService, ProductService>();

            //Entity Framework連線字串請放在這
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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Empty_Signin}/{action=Index}/{id?}");
            });
        }
    }
}
