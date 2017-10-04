using System.Collections.Generic;

namespace HotelBooking.Domain
{
    public class Customer : NameIdPair
    {
        public Customer(int id, string name) : base(id, name)
        {
        }
       
        public string Email { get; set; }
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
