using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public static CustomerViewModel FromCustomer(Customer c)
        {
            var vm = new CustomerViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email
            };
            return vm;
        }
    }
}
