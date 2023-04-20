using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    public interface IEntityService<E> where E : Entity {

        public List<E> GetAll();

        public E? GetByID(Guid id);

        public E Add(E entity);

        public E Update(E entity);

        public void Remove(E entity);
    }
}