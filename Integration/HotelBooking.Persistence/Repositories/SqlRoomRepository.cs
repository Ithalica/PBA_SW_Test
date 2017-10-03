using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Persistence.DataContext;
using HotelBooking.Persistence.Mappers;

namespace HotelBooking.Persistence.Repositories
{
    public class SqlRoomRepository : IRepository<Room>
    {
        private readonly HotelBookingContext _hotelBookingContext;
        private readonly RoomMapper _roomMapper;

        public SqlRoomRepository(HotelBookingContext hotelBookingContext)
        {
            _hotelBookingContext = hotelBookingContext;
            _roomMapper = new RoomMapper();
        }

        public Room Get(int id)
        {
            return _roomMapper.Map(_hotelBookingContext.Room.Find(id));
        }

        public IList<Room> GetAll()
        {
            return _hotelBookingContext.Room.Select(_roomMapper.Map).ToList();
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
