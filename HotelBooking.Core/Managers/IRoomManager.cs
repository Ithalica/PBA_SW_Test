using System.Collections.Generic;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
{
    public interface IRoomManager
    {
        IEnumerable<Room> GetAllRooms();

        Room GetRoom(int id);

        bool DeleteRoom(int id);

        bool TryUpdateRoom(Room item, out Room updatedItem);

        bool TryCreateRoom(Room item, out Room createdItem);
    }
}
