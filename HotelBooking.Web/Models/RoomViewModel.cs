using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public static RoomViewModel FromRoom(Room room)
        {
            var vm = new RoomViewModel
            {
                Id = room.Id,
                Description = room.Description
            };
            return vm;
        }
    }
}
