﻿using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Web.BusinessLogic;
using HotelBooking.Web.Data.Repositories;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Managers
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
            var room = GetAvaliableRoom(booking);

            if (room != null)
            {
                booking.RoomId = room.Id;
                booking.IsActive = true;
                _bookingRepository.Add(booking);
                return true;
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

            int noOfRooms = _roomRepository.GetAll().Count();

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
            return startDate < DateTime.Today || startDate > endDate;
        }

        public Room GetAvaliableRoom(Booking booking)
        {
            //INFO: Replaces old method: FindAvailableRoom 
            Booking[] activeBookings = _bookingRepository.GetAll().Where(b => b.IsActive).ToArray();

            return (from room in _roomRepository.GetAll()
                let activeBookingsForCurrentRoom = activeBookings.Where(b => b.RoomId == room.Id)
                where activeBookingsForCurrentRoom.All(b => booking.StartDate < b.StartDate &&
                                                            booking.EndDate < b.StartDate ||
                                                            booking.StartDate > b.EndDate &&
                                                            booking.EndDate > b.EndDate)
                select room).FirstOrDefault();
        }
    }
}