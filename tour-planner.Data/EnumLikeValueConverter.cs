using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourPlanner.Model;

namespace TourPlanner.Data {

    /// <summary>
    ///     <see cref="ValueConverter{TModel, TProvider}"/> for <see cref="EnumLike{E, V}" />.
    /// </summary>
    public class EnumLikeValueConverter<E, V> : ValueConverter<E?, int> where E : EnumLike<E, V> {

        // e cannot be null:
        // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#configuring-a-value-converter
        public EnumLikeValueConverter(HashSet<E> all) : base(
            e => e!.Id,
            id => EnumLike<E, V>.FromId(id, all)
        ) {
            // noop
        }
    }
}
