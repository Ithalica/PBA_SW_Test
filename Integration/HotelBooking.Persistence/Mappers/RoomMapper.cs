using HotelBooking.Domain;

namespace HotelBooking.Persistence.Mappers
{
    public class RoomMapper
    {
        public Room Map(Entities.Room source)
        {
            if (source == null)
                return null;
            return new Room(source.Id)
            {
                Description = source.Description
            };
        }

        public Entities.Room Map(Room source)
        {
            if (source == null)
                return null;
            return new Entities.Room
            {
                Id = source.Id,
                Description = source.Description,
                //Bookings = //TODO: fix this
            };
        }
    }
}
