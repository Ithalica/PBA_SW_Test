﻿using HotelBooking.Persistence.Entities;

namespace HotelBooking.Persistence.Mappers
{
    public class CustomerMapper
    {
        //private readonly BookingMapper _bookingMapper;

        public CustomerMapper()
        {
            //_bookingMapper = new BookingMapper();
        }

        public Customer Map(Domain.Customer source)
        {
            if (source == null)
                return null;

            return new Customer
            {
                Id = source.Id,
                Name = source.Name,
                Email = source.Email,
                //Bookings = source.Bookings.Select(_bookingMapper.Map).ToList()
            };
        }

        public Domain.Customer Map(Customer source)
        {
            if (source == null)
                return null;

            return new Domain.Customer(source.Id, source.Name)
            {
                Email = source.Email,
                //Bookings = source.Bookings.Select(_bookingMapper.Map).ToList()
            };
        }
    }
}
