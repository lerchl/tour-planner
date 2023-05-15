using TourPlanner.Model;

namespace TourPlanner.Logic.Report {

    /// <summary>
    ///     A service for generating reports - of type <typeparamref name="E"/> - for <see cref="Tour"/>s.
    /// </summary>
    public interface ITourReporter<E> {

        /// <summary>
        ///     Generates a report for a <paramref name="tour"/>.
        /// </summary>
        /// <param name="tour">The <see cref="Tour"/> to generate a report for</param>
        /// <returns>The generated report.</returns>
        public E TourReport(Tour tour);

        /// <summary>
        ///     Generates a report for a list of <paramref name="tours"/>.
        /// </summary>
        /// <param name="tours">The <see cref="Tour"/>s to generate a report for</param>
        /// <returns>The generated report.</returns>
        public E ToursReport(List<Tour> tours);
    }
}
