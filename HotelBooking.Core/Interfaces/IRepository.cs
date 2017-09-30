using System.Collections.Generic;

namespace HotelBooking.Core.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T entity);
        T Edit(T entity);
        bool Remove(int id);
    }
}
