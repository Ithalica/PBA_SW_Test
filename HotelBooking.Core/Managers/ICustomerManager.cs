using System.Collections.Generic;
using HotelBooking.Domain;

namespace HotelBooking.Core.Managers
{
    public interface ICustomerManager
    {
        IEnumerable<Customer> GetAllCustomers();
    }
}
