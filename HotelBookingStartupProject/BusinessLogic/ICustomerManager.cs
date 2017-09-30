using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBookingStartupProject.Models;

namespace HotelBookingStartupProject.BusinessLogic
{
    public interface ICustomerManager
    {
        IEnumerable<Customer> GetAllCustomers();
    }
}
