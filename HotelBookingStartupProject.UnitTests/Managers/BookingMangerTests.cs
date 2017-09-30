using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Web.Managers;
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
