using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     A service for CRUD operations.
    /// </summary>
    public interface ICrudService<E> where E : Entity {

        public List<E> GetAll();

        public E? GetById(Guid id);

        public E Add(E entity);

        public E Update(E entity);

        public void Remove(E entity);
    }
}
