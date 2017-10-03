//using System.Collections.Generic;
//using System.Linq;
//using HotelBooking.Core.Interfaces;
//using HotelBooking.Domain;
//using HotelBooking.Web.Models;
//using Microsoft.EntityFrameworkCore;

//namespace HotelBooking.Web.Data.Repositories
//{
//    public class BookingRepository : IRepository<Booking>
//    {
//        private readonly HotelBookingContext db;

//        public BookingRepository(HotelBookingContext context)
//        {
//            db = context;
//        }

//        public Booking Add(Booking entity)
//        {
//            var newBooking = db.Booking.Add(entity);
//            db.SaveChanges();
//            return newBooking.Entity;
//        }

//        public Booking Edit(Booking entity)
//        {
//            db.Entry(entity).State = EntityState.Modified;
//            db.SaveChanges();
//            return entity;
//        }

//        public Booking Get(int id)
//        {
//            return db.Booking.Include(b => b.Customer).Include(b => b.Room).FirstOrDefault(b => b.Id == id);
//        }

//        public IEnumerable<Booking> GetAll()
//        {
//            return db.Booking.Include(b => b.Customer).Include(b => b.Room).ToList();
//        }

//        public bool Remove(int id)
//        {
//            var booking = db.Booking.FirstOrDefault(b => b.Id == id);
//            db.Booking.Remove(booking);
//            db.SaveChanges();
//            return true;
//        }

//    }
//}
