using System.ComponentModel.DataAnnotations;
using HotelBooking.Domain;

namespace HotelBooking.Web.Models
{
    public class CustomerInputModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public static CustomerInputModel FromCustomer(Customer c)
        {
            return new CustomerInputModel
            {
                Id = c.Id,
                Email = c.Email,
                Name = c.Name
            };
        }
    }
}
