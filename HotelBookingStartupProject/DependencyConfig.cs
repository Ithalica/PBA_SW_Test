using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Web.BusinessLogic;
using HotelBooking.Web.Data;
using HotelBooking.Web.Data.Repositories;
using HotelBooking.Web.Managers;
using HotelBooking.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HotelBooking.Web
{
    public class DependencyConfig
    {
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
    }
}
