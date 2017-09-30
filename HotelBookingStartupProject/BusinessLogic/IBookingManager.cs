using HotelBookingStartupProject.Models;
using System;
using System.Collections.Generic;

namespace HotelBookingStartupProject.BusinessLogic
{
    public interface IBookingManager
    {
        IList<Booking> GetAllBookings();
        (DateTime minDate, DateTime maxDate) GetMinAndMaxBookingDates(IList<Booking> bookings);
        bool CreateBooking(Booking booking);
        List<DateTime> GetFullyOccupiedDates(IList<Booking> bookings, DateTime minDate, DateTime maxDate);
        bool IsBookingDateValid(DateTime startDate, DateTime endDate);
        Room GetAvaliableRoom(Booking booking);
    }
}
