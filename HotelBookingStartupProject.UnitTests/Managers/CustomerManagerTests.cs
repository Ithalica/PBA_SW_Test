using System.Collections.Generic;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using HotelBooking.Core.Managers;

namespace HotelBookingStartupProject.UnitTests.Managers
{
    [TestFixture]
    public class CustomerManagerTests
    {
        private IRepository<Customer> _subRepository;
        private ICustomerManager _customerManager;

        [SetUp]
        public void TestInitialize()
        {
            _subRepository = Substitute.For<IRepository<Customer>>();
            _subRepository.GetAll().Returns(TestCustomerDbEntities());

            _customerManager = CreateManager();
        }

        [TearDown]
        public void TearDown()
        {
            _subRepository = null;
            _customerManager = null;
        }

        [Test]
        public void GetAllCustomers_ThereExistsCustomers_ListContainsElements()
        {
            IEnumerable<Customer> customers = _customerManager.GetAllCustomers();
            Assert.IsNotEmpty(customers);
        }

        [Test]
        public void GetAllCustomers_AllKnowCustomersExists_ListContainsKnownCustomers()
        {
            IEnumerable<Customer> customers = _customerManager.GetAllCustomers();
            IEnumerable<Customer> knownCustomers = TestCustomerDbEntities();

            bool doesNotContainElement = knownCustomers.Any(customer => customers.All(b => b.Id != customer.Id));

            Assert.IsFalse(doesNotContainElement);
        }

        private CustomerManager CreateManager()
        {
            return new CustomerManager(_subRepository);
        }

        private IEnumerable<Customer> TestCustomerDbEntities()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id = 1,
                    Name="John Smith", Email="js@gmail.com"
                },
                new Customer
                {
                    Id = 2,
                    Name="Jane Doe", Email="jd@gmail.com"
                }
            };
        }
    }
}
