using System;
using System.ComponentModel.DataAnnotations;

namespace HotelBooking.Domain
{
    public class Booking : IdentifiableObject
    {
        public Booking(int id) : base(id)
        {
        }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public Customer Customer { get; set; }
        public Room Room { get; set; }
    }
}
