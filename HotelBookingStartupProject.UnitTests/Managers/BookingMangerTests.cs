using System;
using HotelBookingStartupProject.Data.Repositories;
using HotelBookingStartupProject.Managers;
using HotelBookingStartupProject.Models;
using NSubstitute;
using NUnit.Framework;

namespace HotelBookingStartupProject.UnitTests.Managers
{
    [TestFixture]
    public class BookingMangerTests
    {
        private IRepository<Room> subRepositoryRoom;
        private IRepository<Booking> subRepositoryBooking;
        
        [SetUp]
        public void TestInitialize()
        {
            //this.subRepositoryRoom = Substitute.For<IRepository<Room>>();
            //this.subRepositoryBooking = Substitute.For<IRepository<Booking>>();
        }

        //[UnitOfWorkName]_[ScenarioUnderTest] _[ExpectedBehavior].
        
        public void IsBookingDatesValid_StartDateAfterEndDate_Failure()
        {
        
        }

        public void IsBookingDatesValid_StartDateBeforeEndDateAndStartDateAfterToday_Success()
        {

        }

        private BookingManger CreateBookingManger()
        {
            return new BookingManger(
                this.subRepositoryRoom,
                this.subRepositoryBooking);
        }
    }
}
