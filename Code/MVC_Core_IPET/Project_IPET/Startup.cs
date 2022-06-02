using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Project_IPET.Hubs;
using Project_IPET.Models;
using Project_IPET.Models.EF;
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
            // �ҥ� Session
            services.AddSession();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddControllersWithViews();
            services.AddSignalR();

            //DI �̿�`�J (Dependency Injection) 
            //�c���ϥ�Dapper���s�u�覡��DB�A��L�H�ϥΪ�Entity Framework�s�u�r��Щ�b�o��U��A���¡C
            services.AddScoped<IDbConnection, SqlConnection>(serviceProvider =>
            {
                SqlConnection conn = new SqlConnection();
                //�����s�u�r��
                conn.ConnectionString = Configuration.GetConnectionString("MyProjectDbConnectionString");
                return conn;
            });
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IPetService, PetService>();

            //Entity Framework�s�u�r��Щ�b�o
            services.AddDbContext<MyProjectContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MyProjectDbConnectionString"));
            });

            //�۰ʵo�e�l��
            services.Configure<CEmailSettings>(Configuration.GetSection("CEmailSettings"));
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.AddMvc();


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
                    pattern: "{controller=Front_Home}/{action=Index}/{id?}");

                endpoints.MapHub<ChatHub>("/chathub");  //�ҰʪA��
            });
        }
    }
}
