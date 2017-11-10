using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBooking.Persistence.DataContext
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

    public static class DbInitializer
    {
        public static void Initialize(HotelBookingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any bookings.
            if (context.Booking.Any())
            {
                return;   // DB has been seeded
            }

            List<Customer> customers = new List<Customer>
            {
                new Customer { Name="John Smith", Email="js@gmail.com" },
                new Customer { Name="Jane Doe", Email="jd@gmail.com" }
            };

            List<Room> rooms = new List<Room>
            {
                new Room { Description="A" },
                new Room { Description="B" },
                new Room { Description="C" }
            };

            DateTime date = DateTime.Today.AddDays(4);
            List<Booking> bookings = new List<Booking>
            {
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date, EndDate=date.AddDays(14), IsActive=true, CustomerId=1, RoomId=3 },

                new Booking { StartDate=date.AddDays(16), EndDate=date.AddDays(19), IsActive=true, CustomerId=1, RoomId=1 },
                new Booking { StartDate=date.AddDays(16), EndDate=date.AddDays(19), IsActive=true, CustomerId=2, RoomId=2 },
                new Booking { StartDate=date.AddDays(16), EndDate=date.AddDays(19), IsActive=true, CustomerId=1, RoomId=3 }
            };

            context.Customer.AddRange(customers);
            context.Room.AddRange(rooms);
            context.SaveChanges();
            context.Booking.AddRange(bookings);
            context.SaveChanges();
        }
    }
}
