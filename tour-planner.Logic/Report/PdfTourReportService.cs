using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlanner.Model;

using static TourPlanner.Logic.TimeConverter;

namespace TourPlanner.Logic.Report {

    /// <summary>
    ///     <see cref="ITourReporter{E}"/> implementation for <see cref="PdfDocument"/>s.
    /// </summary>
    public class PdfTourReporter : ITourReporter<PdfDocument> {

        private const int DOCUMENT_HEADLINE_FONT_SIZE = 18;
        private const int DOCUMENT_SECOND_LEVEL_HEADLINE_FONT_SIZE = 14;
        private const int DOCUMENT_FONT_SIZE = 12;


        public delegate PdfWriter CreatePdfWriter();

        private readonly CreatePdfWriter _createPdfWriter;

        // /////////////////////////////////////////////////////////////////////////
        // Init
        // /////////////////////////////////////////////////////////////////////////

        public PdfTourReporter(CreatePdfWriter createPdfWriter) {
            _createPdfWriter = createPdfWriter;
        }

        // /////////////////////////////////////////////////////////////////////////
        // Methods
        // /////////////////////////////////////////////////////////////////////////

        public PdfDocument TourReport(Tour tour, List<TourLog> tourLogs) {
            var pdf = new PdfDocument(_createPdfWriter());
            var document = CreateBlankDocumentWithHeadline(pdf, "Tour Report: " + tour.Name);

            document.Add(new Paragraph(tour.Description ?? "No description."));

            var detailsHeadline = new Paragraph("Details");
            detailsHeadline.SetFontSize(DOCUMENT_SECOND_LEVEL_HEADLINE_FONT_SIZE);
            detailsHeadline.SetBold();
            document.Add(detailsHeadline);

            document.Add(new Paragraph("From: " + (tour.From ?? "N/A")));
            document.Add(new Paragraph("To: " + (tour.To ?? "N/A")));
            document.Add(new Paragraph("Transport Type: " + tour.TransportType.Name));
            document.Add(new Paragraph("Distance: " + (tour.Distance == null ? "N/A" : (tour.Distance + "km"))));

            var secondsTimeConverter = new TimeConverter(s => TimeSpan.FromSeconds(s), DAYS, HOURS, MINUTES, SECONDS);
            document.Add(new Paragraph("Estimated Time: " + (tour.EstimatedTime == null ? "N/A" : secondsTimeConverter.Convert(tour.EstimatedTime!.Value))));

            if (tour.MapImage != null) {
                ImageData imageData = ImageDataFactory.Create(tour.MapImage);
                document.Add(new Image(imageData));
            }

            var tourLogsHeadline = new Paragraph("Logs");
            tourLogsHeadline.SetFontSize(DOCUMENT_SECOND_LEVEL_HEADLINE_FONT_SIZE);
            tourLogsHeadline.SetBold();
            document.Add(tourLogsHeadline);

            var tourLogTable = new Table(UnitValue.CreatePercentArray(5)).UseAllAvailableWidth();
            tourLogTable.AddHeaderCell("Date");
            tourLogTable.AddHeaderCell("Duration");
            tourLogTable.AddHeaderCell("Rating");
            tourLogTable.AddHeaderCell("Difficulty");
            tourLogTable.AddHeaderCell("Comment");

            var minutesTimeConverter = new TimeConverter(s => TimeSpan.FromMinutes(s), HOURS, MINUTES, SECONDS);
            tourLogs.ForEach(tourLog => {
                tourLogTable.AddCell(tourLog.Date.ToString("dd.MM.yyyy"));
                tourLogTable.AddCell(minutesTimeConverter.Convert(tourLog.Duration));
                tourLogTable.AddCell(tourLog.Rating.Name);
                tourLogTable.AddCell(tourLog.Difficulty.Name);
                tourLogTable.AddCell(tourLog.Comment ?? "");
            });

            document.Add(tourLogTable);
            return pdf;
        }

        public PdfDocument ToursReport(List<Tour> tours, Dictionary<Tour, List<TourLog>> tourLogs) {
            var pdf = new PdfDocument(_createPdfWriter());
            var document = CreateBlankDocumentWithHeadline(pdf, "Tours Report");

            document.Add(new Paragraph("This report contains the averages of all tour logs of all tours."));

            document.Add(new Paragraph("Available Ratings: " + Rating.ALL.Select(rating => $"{rating.Name} ({rating.Value})").Aggregate((a, b) => a + ", " + b)));
            document.Add(new Paragraph("Available Difficulties: " + Difficulty.ALL.Select(difficulty => $"{difficulty.Name} ({difficulty.Value})").Aggregate((a, b) => a + ", " + b)));

            var table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            table.AddHeaderCell("Name");
            table.AddHeaderCell("Duration");
            table.AddHeaderCell("Difficulty");
            table.AddHeaderCell("Rating");

            var minutesTimeConverter = new TimeConverter(s => TimeSpan.FromMinutes(s), HOURS, MINUTES, SECONDS);
            tours.ForEach(tour => {
                long averageDuration = (long) tourLogs[tour].Select(tourLog => tourLog.Duration).Average();
                double averageDifficulty = tourLogs[tour].Select(tourLog => tourLog.Difficulty.Value).Average();
                double averageRating = tourLogs[tour].Select(tourLog => tourLog.Rating.Value).Average();

                table.AddCell(tour.Name);
                table.AddCell(minutesTimeConverter.Convert(averageDuration));
                table.AddCell(FormatAverage(averageDifficulty, Difficulty.ALL));
                table.AddCell(FormatAverage(averageRating, Rating.ALL));
            });

            document.Add(table);
            return pdf;
        }

        private static string FormatAverage<I>(double average, IEnumerable<EnumLike<I, int>> all) where I : EnumLike<I, int> {
            var list = all.ToList();

            if (average - Math.Truncate(average) == 0) {
                var name = list.Find(difficulty => difficulty.Value == average)!.Name;
                return $"{name} ({average:0.00})";
            }

            string closer;
            string further;

            if (average - Math.Truncate(average) > 0.5) {
                closer = list.Find(difficulty => difficulty.Value == Math.Floor(average))!.Name;
                further = list.Find(difficulty => difficulty.Value == Math.Ceiling(average))!.Name;
            } else {
                closer = list.Find(difficulty => difficulty.Value == Math.Ceiling(average))!.Name;
                further = list.Find(difficulty => difficulty.Value == Math.Floor(average))!.Name;
            }

            return $"More {further} than {closer} ({average:0.00})";
        }

        private static Document CreateBlankDocumentWithHeadline(PdfDocument pdf, string headlineText) {
            var document = new Document(pdf);
            document.SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
            document.SetFontSize(DOCUMENT_FONT_SIZE);

            var headline = new Paragraph(headlineText);
            headline.SetFontSize(DOCUMENT_HEADLINE_FONT_SIZE);
            headline.SetBold();

            document.Add(headline);

            return document;
        }
    }
}
