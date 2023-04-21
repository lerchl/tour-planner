using TourPlanner.Data.Repository;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Logic.Service {

    /// <summary>
    ///     <see cref="ICrudService{E}" /> implementation with <see cref="IValidator{T}" />
    /// </summary>
    public abstract class CrudService<E, R, V> : ICrudService<E>
            where E : Entity
            where R : ICrudRepository<E>
            where V : IValidator<E> {

        protected readonly R _repository;
        protected readonly V _validator;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public CrudService(R repository, V validator) {
            _repository = repository;
            _validator = validator;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public List<E> GetAll() {
            return _repository.GetAll();
        }

        public E? GetById(Guid id) {
            return _repository.GetById(id);
        }

        public E Add(E entity) {
            _validator.ValidateSave(entity);
            return _repository.Add(entity);
        }

        public E Update(E entity) {
            _validator.ValidateSave(entity);
            return _repository.Update(entity);
        }

        public void Remove(E entity) {
            _repository.Remove(entity);
        }
    }
}
