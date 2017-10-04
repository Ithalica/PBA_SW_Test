using System;
using System.Collections.Generic;
using System.Text;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
{
    public class RoomManager : IRoomManager
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomManager(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository;
        }
        
        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }

        public Room GetRoom(int id)
        {
            return _roomRepository.Get(id);
        }

        public bool DeleteRoom(int id)
        {
            return _roomRepository.Delete(id);
        }

        public bool TryUpdateRoom(Room item, out Room updatedItem)
        {
            return _roomRepository.TryUpdate(item, out updatedItem);
        }

        public bool TryCreateRoom(Room item, out Room createdItem)
        {
            return _roomRepository.TryCreate(item, out createdItem);
        }
    }

}
