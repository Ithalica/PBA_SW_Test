using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public CustomerViewModel Customer { get; set; }
        public RoomViewModel Room { get; set; }

        public static BookingViewModel FromBooking(Booking b)
        {
            var vm = new BookingViewModel
            {
                Id = b.Id,
                Customer = CustomerViewModel.FromCustomer(b.Customer),
                EndDate = b.EndDate,
                StartDate = b.StartDate,
                Room = RoomViewModel.FromRoom(b.Room)
            };
            return vm;
        }
    }
}
