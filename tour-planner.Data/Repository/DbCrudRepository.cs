using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     <see cref="Singleton{T}"/> <see cref="ICrudRepository{E}"/> implementation for <see cref="DbContext"/>s.
    /// </summary>
    /// <typeparam name="S">the <see cref="Singleton{T}"/> type (the inheriting class)</typeparam>
    /// <typeparam name="E">the <see cref="Entity"/> type</typeparam>
    /// <typeparam name="C">the <see cref="DbContext"/> type</typeparam>
    public abstract class DbCrudRepository<S, E, C> : Singleton<S>, ICrudRepository<E>
            where S : class, new()
            where E : Entity
            where C : DbContext, new() {

        /// <summary>
        ///     Get the <see cref="DbSet{E}"/> for <typeparamref name="E"/> from the <see cref="DbContext"/>.
        /// </summary>
        abstract protected DbSet<E> GetDbSet(C context);

        // /////////////////////////////////////////////////////////////////////////
        // Implementations
        // /////////////////////////////////////////////////////////////////////////

        public ICollection<E> GetAll() {
            using var context = new C();
            return GetDbSet(context).ToList();
        }

        public E? GetById(Guid id) {
            using var context = new C();
            return GetDbSet(context).Find(id);
        }

        public E Add(E entity) {
            using var context = new C();
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public E Update(E entity) {
            using var context = new C();
            context.Update(entity);
            context.SaveChanges();
            return entity;
        }

        public void Remove(E entity) {
            using var context = new C();
            context.Remove(entity);
            context.SaveChanges();
        }
    }
}
