using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Repository;
using HotelWeb.Utils.FileUploadService;

namespace HotelWeb
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
            services.AddRazorPages();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();
            services.AddSingleton<IRoomTypeRepository, RoomTypeRepository>();
            services.AddSingleton<IServiceRepository, ServiceRepository>();
            services.AddSingleton<IServiceTypeRepository, ServiceTypeRepository>();
            services.AddSingleton<ICouponRepository, CouponRepository>();
            services.AddSingleton<IBookRoomRepository, BookRoomRepository>();
            services.AddSingleton<IRoomInBookingRepository, RoomInBookingRepository>();
            services.AddSingleton<IServiceInRoomRepository, ServiceInRoomRepository>();
            services.AddSingleton<IFileUploadService, LocalFileUploadService>();
            services.AddDistributedMemoryCache(); 
            
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
