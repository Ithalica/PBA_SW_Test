using System.Collections.Generic;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Web.BusinessLogic;
using HotelBooking.Web.Data.Repositories;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.Managers
{
    public class CustomerManager : ICustomerManager
    {
        private readonly IRepository<Customer> _customeRepository;
        public CustomerManager(IRepository<Customer> customeRepository)
        {
            _customeRepository = customeRepository;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customeRepository.GetAll();
        }
    }
}
