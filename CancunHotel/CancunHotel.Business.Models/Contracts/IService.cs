using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancunHotel.Business.Models.Contracts
{
    /// <summary>
    /// Service contract for basic CRUD operations against <typeparamref name="TEntity"/> and <typeparamref name="TModel"/>.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IService<TModel, TEntity>
        where TModel : EntityModel
        where TEntity : Entity
    {
        /// <summary>
        /// Gets the specified <typeparamref name="TModel"/> by the ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <typeparamref name="TModel"/> instance, or null, if not found.</returns>
        TModel GetByID(Guid id);

        /// <summary>
        /// Gets all instance of <typeparamref name="TModel"/>.
        /// </summary>
        /// <returns>A list of <typeparamref name="TModel"/> objects.</returns>
        List<TModel> GetAll();

        /// <summary>
        /// Creates the specified <typeparamref name="TModel"/> model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The created <typeparamref name="TModel"/>.</returns>
        TModel Create(TModel model);

        /// <summary>
        /// Creates a range of <typeparamref name="TModel"/> objects.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns><c>true</c> if successful, otherwise, <c>false</c></returns>
        bool CreateRange(List<TModel> list);

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns><c>true</c> if successful, otherwise, <c>false</c></returns>
        bool Update(TModel model);

        /// <summary>
        /// Deletes the <typeparamref name="TEntity"/> instance with the specified ID.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns><c>true</c> if successful, otherwise, <c>false</c></returns>
        bool Delete(Guid id);
    }
}
