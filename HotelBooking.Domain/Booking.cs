using System;

namespace HotelBooking.Domain
{
    public class Booking : IdentifiableObject
    {
        public Booking(int id) : base(id)
        {
        }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
        public Room Room { get; set; }
    }
}
