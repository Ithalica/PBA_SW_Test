using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Common.Mapping;
using HotelBooking.Domain;

namespace HotelBooking.Persistence.Mappers
{
    public class RoomMapper : IMapper<Entities.Room, Domain.Room>, IMapper<Domain.Room, Entities.Room>
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
