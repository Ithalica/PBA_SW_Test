using System.Collections.Generic;

namespace HotelBooking.Core.Interfaces
{
    /// <summary>
    /// Provides default functionality for interacting with a data source
    /// </summary>
    /// <typeparam name="T">Type of object to manage</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Gets the specified entity.
        /// </summary>
        /// <param name="id">The identifier of the entity.</param>
        /// <returns>Entity matching the id.</returns>
        T Get(int id);

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>Collection of entities.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Tries the create new entity.
        /// </summary>
        /// <param name="item">The entity to create.</param>
        /// <param name="createdItem">The created entity.</param>
        /// <returns>Boolean value. True if successfully created, else false.</returns>
        bool TryCreate(T item, out T createdItem);

        /// <summary>
        /// Tries the update the entity.
        /// </summary>
        /// <param name="item">The entity to update.</param>
        /// <param name="updatedItem">The updated entity.</param>
        /// <returns>Boolean value. True if successfully updated, else false.</returns>
        bool TryUpdate(T item, out T updatedItem);

        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>Boolean value. True if successfully deleted, else false.</returns>
        bool Delete(int id);
    }
}
