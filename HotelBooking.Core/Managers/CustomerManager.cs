using System.Collections.Generic;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
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

        public Customer GetCustomer(int id)
        {
            return _customeRepository.Get(id);
        }

        public bool DeleteCustomer(int id)
        {
            return _customeRepository.Delete(id);
        }

        public bool TryUpdateCustomer(Customer item, out Customer updatedItem)
        {
            return _customeRepository.TryUpdate(item, out updatedItem);
        }

        public bool TryCreateCustomer(Customer item, out Customer createdItem)
        {
            return _customeRepository.TryCreate(item, out createdItem);
        }
    }
}
