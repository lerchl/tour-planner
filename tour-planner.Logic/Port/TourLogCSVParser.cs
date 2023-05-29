using TourPlanner.Logic.Service;
using TourPlanner.Model;

using static TourPlanner.Logic.DateUtils;

namespace TourPlanner.Logic.Port {

    /// <summary>
    ///     Implementation of <see cref="ITourLogCSVParser"/> that uses semicolons as separators.
    /// </summary>
    public class TourLogCSVParser : ITourLogCSVParser {

        private readonly ITourService _tourService;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public TourLogCSVParser(ITourService tourService) {
            _tourService = tourService;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public string Serialize(List<TourLog> list) {
            return list.Select(tourLog => $"{tourLog.Id};" +
                                          $"{FormatDateTime(tourLog.DateTime, DATE_TIME_FORMAT_WITHOUT_SECONDS)};" + 
                                          $"{tourLog.Duration};" +
                                          $"{tourLog.Rating.Value};" +
                                          $"{tourLog.Difficulty.Value};" +
                                          $"{tourLog.Tour!.Id}")
                       .Aggregate((a, b) => $"{a}\n{b}");
        }

        public List<TourLog> Deserialize(string str) {
            return str.Split("\n").Select(line => {
                var parts = line.Split(";");

                return new TourLog {
                    Id = Guid.Parse(parts[0]),
                    DateTime = ParseDateTime(parts[1], DATE_TIME_FORMAT_WITHOUT_SECONDS),
                    Duration = long.Parse(parts[2]),
                    Rating = Rating.FromValue(int.Parse(parts[3]), Rating.ALL),
                    Difficulty = Difficulty.FromValue(int.Parse(parts[4]), Difficulty.ALL),
                    Tour = _tourService.GetById(Guid.Parse(parts[5]))
                };
            }).ToList();
        }
    }
}
