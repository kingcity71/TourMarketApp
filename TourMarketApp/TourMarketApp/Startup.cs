using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TourMarket.BLL;
using TourMarket.DAL;
using TourMarket.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace TourMarketApp
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
            services.AddMvc();
            services.AddSession();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Context>(options => options.UseSqlServer(connection));
            // установка конфигурации подключения
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => //CookieAuthenticationOptions
        {
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                    });
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICityService, CityService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IHotelService, HotelService>();
            services.AddTransient<IBasketService, BasketService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
