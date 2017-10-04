using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Persistence.DataContext;
using HotelBooking.Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HotelBooking.Persistence.Repositories
{
    public class SqlRoomRepository : IRepository<Room>
    {
        private readonly HotelBookingContext _hotelBookingContext;

        public SqlRoomRepository(HotelBookingContext hotelBookingContext)
        {
            _hotelBookingContext = hotelBookingContext;
        }

        public Room Get(int id)
        {
            var mapper = new RoomMapper();
            return mapper.Map(_hotelBookingContext.Room.Find(id));
        }

        public IList<Room> GetAll()
        {
            var mapper = new RoomMapper();
            return _hotelBookingContext.Room.Select(mapper.Map).ToList();
        }

        public bool TryCreate(Room item, out Room createdItem)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(Room item, out Room updatedItem)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
