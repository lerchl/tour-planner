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

        public PdfDocument TourReport(Tour tour) {
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
            tourLogTable.AddHeaderCell("Time");
            tourLogTable.AddHeaderCell("Rating");
            tourLogTable.AddHeaderCell("Difficulty");
            tourLogTable.AddHeaderCell("Comment");

            var minutesTimeConverter = new TimeConverter(s => TimeSpan.FromMinutes(s), HOURS, MINUTES, SECONDS);
            tour.TourLogs.ForEach(tourLog => {
                tourLogTable.AddCell(tourLog.Date.ToString("dd.MM.yyyy"));
                tourLogTable.AddCell(minutesTimeConverter.Convert(tourLog.Time));
                tourLogTable.AddCell(tourLog.Rating.Name);
                tourLogTable.AddCell(tourLog.Difficulty.Name);
                tourLogTable.AddCell(tourLog.Comment ?? "");
            });

            document.Add(tourLogTable);
            return pdf;
        }

        public PdfDocument ToursReport(List<Tour> tours) {
            throw new NotImplementedException();
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
