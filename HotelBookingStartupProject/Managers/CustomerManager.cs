using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingStartupProject.BusinessLogic;
using HotelBookingStartupProject.Data.Repositories;
using HotelBookingStartupProject.Models;

namespace HotelBookingStartupProject.Managers
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
