using System.Collections.Generic;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;

namespace HotelBooking.Persistence.Repositories
{
    public class SqlBookingRepository : IRepository<Booking>
    {
        public IEnumerable<Booking> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Booking Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public Booking Add(Booking entity)
        {
            throw new System.NotImplementedException();
        }

        public Booking Edit(Booking entity)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
