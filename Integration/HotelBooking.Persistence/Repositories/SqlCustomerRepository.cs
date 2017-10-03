using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelBooking.Core.Interfaces;
using HotelBooking.Domain;
using HotelBooking.Persistence.DataContext;
using HotelBooking.Persistence.Mappers;

namespace HotelBooking.Persistence.Repositories
{
    public class SqlCustomerRepository: IRepository<Customer>
    {
        private readonly HotelBookingContext _hotelBookingContext;
        private readonly CustomerMapper _customerMapper;

        public SqlCustomerRepository(HotelBookingContext hotelBookingContext)
        {
            _hotelBookingContext = hotelBookingContext;
            _customerMapper = new CustomerMapper();
        }

        public Customer Get(int id)
        {
            return _customerMapper.Map(_hotelBookingContext.Customer.Find(id));
        }

        public IList<Customer> GetAll()
        {
            return _hotelBookingContext.Customer.Select(_customerMapper.Map).ToList();
        }

        public bool TryCreate(Customer item, out Customer createdItem)
        {
            throw new NotImplementedException();
        }

        public bool TryUpdate(Customer item, out Customer updatedItem)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
