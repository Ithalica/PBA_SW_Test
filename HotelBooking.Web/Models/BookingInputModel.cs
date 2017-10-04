using System;
using System.ComponentModel.DataAnnotations;
using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class BookingInputModel
    {
        public int Id { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public int RoomId { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public static BookingInputModel FromBooking(Booking b)
        {
            var im = new BookingInputModel
            {
                Id = b.Id,
                CustomerId = b.Customer.Id,
                EndDate = b.EndDate,
                StartDate = b.StartDate,
                RoomId = b.Room.Id,
                IsActive = b.IsActive
            };
            return im;
        }
    }
}
