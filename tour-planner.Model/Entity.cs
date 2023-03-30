using System.ComponentModel.DataAnnotations;

namespace TourPlanner.Model {

    /// <summary>
    ///     Base class for all entities.
    /// </summary>
    public abstract class Entity {

        public abstract Guid GetGuid();
    }
}
