namespace HotelBooking.Domain
{
    public class Room : IdentifiableObject
    {
        public Room(int id) : base(id)
        {

        }
        public string Description { get; set; }
    }
}
