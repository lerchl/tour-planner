using Microsoft.EntityFrameworkCore;
using TourPlanner.Model;

namespace TourPlanner.Data.Repository {

    /// <summary>
    ///     <see cref="Singleton{T}"/> <see cref="ICrudRepository{E}"/> implementation for <see cref="DbContext"/>s.
    /// </summary>
    /// <typeparam name="E">the <see cref="Entity"/> type</typeparam>
    /// <typeparam name="C">the <see cref="DbContext"/> type</typeparam>
    public abstract class DbCrudRepository<E, C> : ICrudRepository<E>
            where E : Entity
            where C : DbContext, new() {

        /// <summary>
        ///     Get the <see cref="DbSet{E}"/> for <typeparamref name="E"/> from the <see cref="DbContext"/>.
        /// </summary>
        abstract protected DbSet<E> GetDbSet(C context);

        // /////////////////////////////////////////////////////////////////////////
        // Implementations
        // /////////////////////////////////////////////////////////////////////////

        public List<E> GetAll() {
            using var context = new C();
            DbSet<E> dbSet = GetDbSet(context);
            dbSet.Load();
            return dbSet.ToList();
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
