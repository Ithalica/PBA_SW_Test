using HotelBooking.Persistence.Entities;

namespace HotelBooking.Persistence.Mappers
{
    public class BookingMapper
    {
        private readonly CustomerMapper _customerMapper;
        private readonly RoomMapper _roomMapper;

        public BookingMapper()
        {
            _customerMapper = new CustomerMapper();
            _roomMapper = new RoomMapper();
        }

        public Domain.Booking Map(Booking source)
        {
            if (source == null)
                return null;
            return new Domain.Booking(source.Id)
            {
                StartDate = source.StartDate,
                IsActive = source.IsActive,
                EndDate = source.EndDate,
                Customer = _customerMapper.Map(source.Customer),
                Room = _roomMapper.Map(source.Room),
            };
        }

        public Booking Map(Domain.Booking source)
        {
            if (source == null)
                return null;
            return new Booking
            {
                Id = source.Id,
                IsActive = source.IsActive,
                StartDate = source.StartDate,
                EndDate = source.EndDate,
                CustomerId = source.Customer.Id,
                RoomId = source.Room.Id,
                Customer = _customerMapper.Map(source.Customer),
                Room = _roomMapper.Map(source.Room)
            };
        }
    }
}
