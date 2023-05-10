using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourPlanner.Model;

namespace TourPlanner.Data.Converter {

    /// <summary>
    ///     <see cref="ValueConverter{TModel, TProvider}"/> for <see cref="Rating"/>.
    /// </summary>
    public class RatingConverter : ValueConverter<Rating?, int> {

        // Rating cannot be null:
        // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#configuring-a-value-converter
        public RatingConverter() : base(
            r => r!.Id,
            id => Rating.FromId(id)
        ) {
            // noop
        }
    }
}
