using TourPlanner.Model;

namespace TourPlanner.Logic.Port {

    public class TourCSVParser : ITourCSVParser {

        public string Serialize(List<Tour> list) {
            return list.Select(tour => $"{tour.Id};" +
                                       $"{tour.Name};" +
                                       $"{tour.Description};" +
                                       $"{tour.From};" +
                                       $"{tour.To};" +
                                       $"{tour.TransportType.Value}")
                       .Aggregate((a, b) => $"{a}\n{b}");
        }

        public List<Tour> Deserialize(string str) {
            return str.Split("\n").Select(line => {
                var parts = line.Split(";");

                return new Tour {
                    Id = Guid.Parse(parts[0]),
                    Name = parts[1],
                    Description = parts[2],
                    From = parts[3],
                    To = parts[4],
                    TransportType = TransportType.FromValue(parts[5], TransportType.ALL)
                };
            }).ToList();
        }
    }
}
