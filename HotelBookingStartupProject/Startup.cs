using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Web.BusinessLogic;
using HotelBooking.Web.Data;
using HotelBooking.Web.Data.Repositories;
using HotelBooking.Web.Managers;
using HotelBooking.Web.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Web
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
            //services.AddDbContext<HotelBookingContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("HotelBookingContext")));

            services.AddDbContext<HotelBookingContext>(opt => opt.UseInMemoryDatabase("HotelBookingDb"));
            services.AddScoped<IRepository<Room>, RoomRepository>();
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Booking>, BookingRepository>();
            services.AddScoped<IBookingManager, BookingManger>();
            services.AddScoped<ICustomerManager, CustomerManager>();

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
                    template: "{controller=Bookings}/{action=Index}/{id?}");
            });
        }
    }
}
