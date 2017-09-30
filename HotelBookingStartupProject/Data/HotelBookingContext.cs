using HotelBooking.Domain;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Web.Data
{
    public class HotelBookingContext : DbContext
    {
        public HotelBookingContext(DbContextOptions<HotelBookingContext> options)
            : base(options)
        {
            DbInitializer.Initialize(this);
        }

        public DbSet<Booking> Booking { get; set; }

        public DbSet<Room> Room { get; set; }

        public DbSet<Customer> Customer { get; set; }
    }
}
