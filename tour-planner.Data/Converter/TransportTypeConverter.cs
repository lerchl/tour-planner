using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourPlanner.Model;

namespace TourPlanner.Data.Converter {

    /// <summary>
    ///     <see cref="ValueConverter{TModel, TProvider}"/> for <see cref="TransportType"/>.
    /// </summary>
    public class TransportTypeConverter : ValueConverter<TransportType?, int> {

        // TransportType cannot be null:
        // https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations#configuring-a-value-converter
        public TransportTypeConverter() : base(
            tt => tt!.Id,
            id => TransportType.FromId(id)
        ) {
            // noop
        }
    }
}
