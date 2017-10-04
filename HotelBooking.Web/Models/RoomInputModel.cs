using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class RoomInputModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static RoomInputModel FromRoom(Room r)
        {
            return new RoomInputModel
            {
                Id = r.Id,
                Description = r.Description
            };
        }

    }
}
