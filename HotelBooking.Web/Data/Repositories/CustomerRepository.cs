//using System;
//using System.Collections.Generic;
//using System.Linq;
//using HotelBooking.Core.Interfaces;
//using HotelBooking.Domain;
//using HotelBooking.Web.Models;

//namespace HotelBooking.Web.Data.Repositories
//{
//    public class CustomerRepository : IRepository<Customer>
//    {
//        private readonly HotelBookingContext db;

//        public CustomerRepository(HotelBookingContext context)
//        {
//            db = context;
//        }

//        public Customer Add(Customer entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Customer Edit(Customer entity)
//        {
//            throw new NotImplementedException();
//        }

//        public Customer Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public IEnumerable<Customer> GetAll()
//        {
//            return db.Customer.ToList();
//        }

//        public bool Remove(int id)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
