using System.Collections.Generic;
using HotelBooking.Domain;
using HotelBooking.Web.Models;

namespace HotelBooking.Web.BusinessLogic
{
    public interface ICustomerManager
    {
        IEnumerable<Customer> GetAllCustomers();
    }
}
