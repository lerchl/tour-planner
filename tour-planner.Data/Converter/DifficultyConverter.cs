using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourPlanner.Model;

namespace TourPlanner.Data.Converter {

    /// <summary>
    ///     <see cref="ValueConverter{TModel, TProvider}"/> for <see cref="Difficulty"/>.
    /// </summary>
    public class DifficultyConverter : ValueConverter<Difficulty?, int> {

        // Rating cannot be null:
        // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#configuring-a-value-converter
        public DifficultyConverter() : base(
            d => d!.Id,
            id => Difficulty.FromId(id)
        ) {
            // noop
        }
    }
}
