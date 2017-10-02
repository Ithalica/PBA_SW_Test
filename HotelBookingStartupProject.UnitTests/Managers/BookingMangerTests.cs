using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Core.Managers;
using NSubstitute;

namespace HotelBookingStartupProject.UnitTests.Managers
{
    [TestFixture]
    public class BookingMangerTests
    {
        private IRepository<Room> _subRepositoryRoom;
        private IRepository<Booking> _subRepositoryBooking;
        private IBookingManager _bookingManager;

        [SetUp]
        public void TestInitialize()
        {
            _subRepositoryRoom = Substitute.For<IRepository<Room>>();
            _subRepositoryRoom.GetAll().Returns(TestRoomDbEntities());

            _subRepositoryBooking = Substitute.For<IRepository<Booking>>();
            _subRepositoryBooking.GetAll().Returns(TestBookingDbEntities());

            _bookingManager = CreateBookingManger();
        }

        [TearDown]
        public void TearDown()
        {
            _subRepositoryBooking = null;
            _subRepositoryRoom = null;
            _bookingManager = null;
        }

        //[UnitOfWorkName]_[ScenarioUnderTest]_[ExpectedBehavior]
        [Test]
        public void CreateBooking_NoAvaliableRoom_Failure()
        {
            Booking booking = new Booking()
            {
                StartDate = new DateTime(2016, 01, 01),
                EndDate = new DateTime(2016, 01, 10)
            };

            bool bookingCreated = _bookingManager.CreateBooking(booking);

            Assert.IsFalse(bookingCreated);
        }

        [Test]
        public void CreateBooking_NoAvaliableRoom_Success()
        {
            Booking booking = new Booking()
            {
                StartDate = new DateTime(2016, 02, 01),
                EndDate = new DateTime(2016, 02, 10)
            };

            bool bookingCreated = _bookingManager.CreateBooking(booking);

            Assert.IsTrue(bookingCreated);
        }

        [Test]
        public void GetAvaliableRoom_NoAvaliableRoom_IsNull()
        {

            DateTime startDate = new DateTime(2016, 01, 01);
            DateTime endDate = new DateTime(2016, 01, 10);

            Room room = _bookingManager.GetAvaliableRoom(startDate, endDate);

            Assert.IsNull(room);
        }

        [Test]
        public void GetAvaliableRoom_AvaliableRoom_IsNotNull()
        {
            DateTime startDate = new DateTime(2016, 02, 01);
            DateTime endDate = new DateTime(2016, 02, 10);

            Room room = _bookingManager.GetAvaliableRoom(startDate, endDate);

            Assert.IsNotNull(room);
        }

        [Test]
        public void GetFullyOccupiedDates_NoBookings_EmptyList()
        {
            DateTime minDate = new DateTime(2017, 01, 01);
            DateTime maxDate = new DateTime(2017, 01, 08);

            List<Booking> bookings = _subRepositoryBooking.GetAll().ToList();

            List<DateTime> collection = _bookingManager.GetFullyOccupiedDates(bookings, minDate, maxDate);

            Assert.IsEmpty(collection);
        }

        [Test]
        public void GetFullyOccupiedDates_WithBookings_ListContainsElements()
        {
            DateTime minDate = new DateTime(2016, 01, 01);
            DateTime maxDate = new DateTime(2016, 01, 08);

            List<Booking> bookings = _subRepositoryBooking.GetAll().ToList();

            List<DateTime> collection = _bookingManager.GetFullyOccupiedDates(bookings, minDate, maxDate);

            Assert.IsNotEmpty(collection);
        }

        [Test]
        public void GetAllBookings_ThereExistsBookings_ListContainsElements()
        {
            IList<Booking> bookings = _bookingManager.GetAllBookings();

            Assert.IsNotEmpty(bookings);
        }

        [Test]
        public void GetAllBookings_AllKnowBookingsExists_ListContainsKnownBookings()
        {
            IList<Booking> bookings = _bookingManager.GetAllBookings();
            IList<Booking> knownBookings = TestBookingDbEntities();

            bool doesNotContainElement = knownBookings.Any(booking => bookings.All(b => b.Id != booking.Id));

            Assert.IsFalse(doesNotContainElement);
        }

        [Test]
        public void IsBookingDatesValid_StartDateAfterEndDate_Failure()
        {
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today;
            var result = _bookingManager.IsBookingDateValid(startDate, endDate);

            Assert.IsFalse(result);
        }

        [Test]
        public void IsBookingDatesValid_StartDateBeforeEndDateAndStartDateAfterToday_Success()
        {
            var startDate = DateTime.Today.AddDays(1);
            var endDate = DateTime.Today.AddDays(2);
            var result = _bookingManager.IsBookingDateValid(startDate, endDate);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsBookingDatesValid_StartDateBeforeToday_Faliure()
        {
            var startDate = DateTime.Today.AddDays(-1);
            var endDate = DateTime.Today;
            var result = _bookingManager.IsBookingDateValid(startDate, endDate);
            Assert.IsFalse(result);
        }

        private BookingManger CreateBookingManger()
        {
            return new BookingManger(_subRepositoryRoom, _subRepositoryBooking);
        }

        private static IList<Room> TestRoomDbEntities()
        {
            List<Room> rooms = new List<Room>()
            {
                new Room
                {
                    Id = 1,
                    Description = "A"
                },

                new Room
                {
                    Id = 2,
                    Description = "B"
                },

                new Room
                {
                    Id = 3,
                    Description = "C"
                }
            };

            return rooms;
        }

        private static IList<Booking> TestBookingDbEntities()
        {
            List<Booking> bookings = new List<Booking>()
            {
                new Booking
                {
                    Id = 1,
                    StartDate = new DateTime(2016, 01, 01),
                    EndDate = new DateTime(2016, 01, 08),
                    IsActive = true,
                    CustomerId = 1,
                    RoomId = 1
                },
                new Booking
                {
                    Id = 2,
                    StartDate = new DateTime(2016, 01, 01),
                    EndDate = new DateTime(2016, 01, 08),
                    IsActive = true,
                    CustomerId = 1,
                    RoomId = 2
                },
                new Booking
                {
                    Id = 3,
                    StartDate = new DateTime(2016, 01, 01),
                    EndDate = new DateTime(2016, 01, 08),
                    IsActive = true,
                    CustomerId = 1,
                    RoomId = 3
                },
                new Booking
                {
                    Id = 4,
                    StartDate = new DateTime(2016, 03, 03),
                    EndDate = new DateTime(2016, 03, 10),
                    IsActive = true,
                    CustomerId = 2,
                    RoomId = 2
                },
                new Booking
                {
                    Id = 5,
                    StartDate = new DateTime(2016, 01, 01),
                    EndDate = new DateTime(2016, 01, 10),
                    IsActive = true,
                    CustomerId = 1,
                    RoomId = 3
                }
            };

            return bookings;
        }
    }
}
