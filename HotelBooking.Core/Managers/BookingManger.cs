using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
{
    public class BookingManger : IBookingManager
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Room> _roomRepository;

        public BookingManger(IRepository<Room> roomRepository, IRepository<Booking> bookingRepository)
        {
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }


        public IList<Booking> GetAllBookings()
        {
            return _bookingRepository.GetAll().ToList();
        }

        public bool CreateBooking(Booking booking)
        {
            var room = GetAvailableRoom(booking.StartDate, booking.EndDate);
            if (room != null)
            {
                booking.Room = room;
                booking.IsActive = true;
                return _bookingRepository.TryCreate(booking, out var _);
            }
            return false;
        }


        public (DateTime minDate, DateTime maxDate) GetMinAndMaxBookingDates(IList<Booking> bookings)
        {
            var bookingStartDates = bookings.Select(b => b.StartDate);
            DateTime minBookingDate = bookingStartDates.Any() ? bookingStartDates.Min() : DateTime.MinValue;

            var bookingEndDates = bookings.Select(b => b.EndDate);
            DateTime maxBookingDate = bookingEndDates.Any() ? bookingEndDates.Max() : DateTime.MaxValue;

            return (minBookingDate, maxBookingDate);
        }

        public List<DateTime> GetFullyOccupiedDates(IList<Booking> bookings, DateTime minDate, DateTime maxDate)
        {

            var fullyOccupiedDates = new List<DateTime>();

            int noOfRooms = _roomRepository.GetAll().Count;

            if (bookings.Any())
            {
                for (DateTime d = minDate; d <= maxDate; d = d.AddDays(1))
                {
                    IEnumerable<Booking> noOfBookings = from b in bookings
                                                        where b.IsActive && d >= b.StartDate && d <= b.EndDate
                                                        select b;
                    if (noOfBookings.Count() >= noOfRooms)
                        fullyOccupiedDates.Add(d);
                }
            }
            return fullyOccupiedDates;
        }

        public bool IsBookingDateValid(DateTime startDate, DateTime endDate)
        {
            return startDate > DateTime.Today && startDate < endDate;
        }

        //INFO: Replaces old method: FindAvailableRoom 
        public Room GetAvailableRoom(DateTime startDate, DateTime endDate)
        {
            Booking[] activeBookings = _bookingRepository.GetAll().Where(b => b.IsActive).ToArray();

            return (from room in _roomRepository.GetAll()
                    let activeBookingsForCurrentRoom = activeBookings.Where(b => b.Room == room)
                    where activeBookingsForCurrentRoom.All(b => startDate < b.StartDate &&
                                                                endDate < b.StartDate ||
                                                                startDate > b.EndDate &&
                                                                endDate > b.EndDate)
                    select room).FirstOrDefault();
        }
    }
}