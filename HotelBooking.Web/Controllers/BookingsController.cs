using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using HotelBooking.Core.Managers;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Controllers
{
    public class BookingsController : Controller
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IBookingManager _bookingManager;
        private readonly ICustomerManager _customerManager;
        private readonly IRepository<Room> _roomRepository;

        public BookingsController(IRepository<Booking> bookingRepos, IRepository<Room> roomRepos, IBookingManager bookingManager, ICustomerManager customerManager)
        {
            _bookingRepository = bookingRepos;
            _roomRepository = roomRepos;
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

            return View(bookings.Select(BookingViewModel.FromBooking).ToList());
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

            var vm = BookingViewModel.FromBooking(booking);

            return View(vm);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["Customer"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("StartDate,EndDate,CustomerId")] BookingInputModel booking)
        {
            if (ModelState.IsValid)
            {
                if (!_bookingManager.IsBookingDateValid(booking.StartDate, booking.EndDate))
                {
                    ViewData["Customer"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
                    ViewBag.Status = "The start date cannot be in the past or later than the end date.";


                    return View(booking);
                }

                var bookings = new Booking(0)
                {
                    StartDate = booking.StartDate,
                    EndDate = booking.EndDate,
                    Customer = _customerManager.GetAllCustomers().FirstOrDefault(c => c.Id == booking.CustomerId),
                };

                if (_bookingManager.CreateBooking(bookings))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            ViewData["Customer"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.CustomerId);
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
            ViewData["Customer"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", booking.Customer.Id);
            ViewData["Room"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", booking.Room.Id);

            //TODO: implement mapper
            var im = BookingInputModel.FromBooking(booking);
            return View(im);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("StartDate,EndDate,IsActive,Customer,Room")] BookingInputModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                Booking booking = _bookingManager.GetAllBookings().FirstOrDefault(x => x.Id == model.Id);
                if (booking == null)
                {
                    return NotFound();
                }
                Booking b;
                try
                {
                    booking.EndDate = model.EndDate;
                    booking.StartDate = model.StartDate;
                    //booking.IsActive = model.IsActive;
                    if (!_bookingRepository.TryUpdate(booking, out b))
                    {
                        //Provide feedback to user
                    };
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_bookingRepository.Get(model.Id) == null)
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customer"] = new SelectList(_customerManager.GetAllCustomers(), "Id", "Name", model.CustomerId);
            ViewData["Room"] = new SelectList(_roomRepository.GetAll(), "Id", "Description", model.RoomId);
            return View(model);
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
            
            var vm = BookingViewModel.FromBooking(booking);

            return View(vm);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookingRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
