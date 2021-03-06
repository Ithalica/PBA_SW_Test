﻿using System.Collections.Generic;

namespace HotelBooking.Persistence.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
