using HotelBooking.Core.Interfaces;
using HotelBooking.Persistence.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HotelBooking.Domain;
using HotelBooking.Persistence.Mappers;

namespace HotelBooking.Persistence.Repositories
{
    public class SqlBookingRepository : IRepository<Booking>
    {
        private readonly HotelBookingContext _dbContext;
        private readonly BookingMapper _bookingMapper;

        public SqlBookingRepository(HotelBookingContext dbContext)
        {
            _dbContext = dbContext;
            _bookingMapper = new BookingMapper();
        }

        public IList<Booking> GetAll()
        {
            return _dbContext.Booking.Include(b => b.Customer).Include(b => b.Room).Select(_bookingMapper.Map).ToList();
        }

        public Booking Get(int id)
        {
            return _bookingMapper.Map(_dbContext.Booking.Include(b => b.Customer).Include(b => b.Room).FirstOrDefault(x => x.Id == id));
        }

        public bool TryCreate(Booking entity, out Booking createdEntity)
        {
            Entities.Booking dbBooking = _bookingMapper.Map(entity);

            _dbContext.Booking.Add(dbBooking);
            _dbContext.SaveChanges();
            createdEntity = Get(dbBooking.Id);
            return true;
        }

        public bool TryUpdate(Booking entity, out Booking updatedEntity)
        {
            var item = _dbContext.Booking.Find(entity.Id);
            item.EndDate = entity.EndDate;
            item.StartDate = entity.StartDate;
            item.IsActive = entity.IsActive;
            _dbContext.SaveChanges();
            updatedEntity = Get(item.Id);
            return true;
        }

        public bool Delete(int id)
        {
            var item = _dbContext.Booking.Find(id);
            _dbContext.Entry(item).State = EntityState.Deleted;
            _dbContext.SaveChanges();
            return true;
        }
    }
}
