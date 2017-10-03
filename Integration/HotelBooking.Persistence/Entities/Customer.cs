using System.Collections.Generic;

namespace HotelBooking.Persistence.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
