using System;
using System.Collections.Generic;
using System.Linq;
using HotelBookingStartupProject.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBookingStartupProject.Models;
using HotelBookingStartupProject.Data.Repositories;

namespace HotelBookingStartupProject.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IBookingManager _bookingManager;
        private readonly ICustomerManager _customerManager;
        private readonly IRepository<Room> _roomRepository;

        public BookingsController(IRepository<Booking> bookingRepos, IRepository<Room> roomRepos, IRepository<Customer> customerRepos, IBookingManager bookingManager, ICustomerManager customerManager)
        {
            _bookingRepository = bookingRepos;
            _roomRepository = roomRepos;
            _customerRepository = customerRepos;
            _bookingManager = bookingManager;
            _customerManager = customerManager;
        }

        // GET: Bookings
        public IActionResult Index(int? id)
        {
            var bookings = _bookingManager.GetAllBookings();

            (DateTime minDate, DateTime maxDate) minAndMaxBookingDates = _bookingManager.GetMinAndMaxBookingDates(bookings);

            ViewBag.FullyOccupiedDates = _bookingManager.GetFullyOccupiedDates(bookings, minAndMaxBookingDates.minDate, minAndMaxBookingDates.maxDate);

            int minBookingYear = minAndMaxBookingDates.minDate.Year;
            int maxBookingYear = minAndMaxBookingDates.maxDate.Year;
            if (id == null)
                id = DateTime.Today.Year;
            else if (id < minBookingYear)
                id = minBookingYear;
            else if (id > maxBookingYear)
                id = maxBookingYear;

            ViewBag.YearToDisplay = id;

            return View(bookings);
        }

        // GET: Bookings/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking booking = _bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StartDate,EndDate,CustomerId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (!_bookingManager.IsBookingDateValid(booking.StartDate, booking.EndDate))
                {
                    ViewData["CustomerId"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
                    ViewBag.Status = "The start date cannot be in the past or later than the end date.";

                    return View(booking);
                }

                if (_bookingManager.CreateBooking(booking))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["CustomerId"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
            ViewBag.Status = "The booking could not be created. There were no available room.";
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking booking = _bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
            ViewData["RoomId"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StartDate,EndDate,IsActive,CustomerId,RoomId")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookingRepository.Edit(booking);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_bookingRepository.Get(booking.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
            ViewData["RoomId"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Booking booking = _bookingRepository.Get(id.Value);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookingRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
