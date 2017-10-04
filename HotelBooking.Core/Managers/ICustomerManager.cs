using System.Collections.Generic;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
{
    public interface ICustomerManager
    {
        IEnumerable<Customer> GetAllCustomers();

        Customer GetCustomer(int id);

        bool DeleteCustomer(int id);

        bool TryUpdateCustomer(Customer item, out Customer updatedItem);

        bool TryCreateCustomer(Customer item, out Customer createdItem);
    }
}
