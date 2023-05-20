using TourPlanner.Logic;

using static TourPlanner.Logic.TimeConverter;

namespace TourPlanner.Test {

    internal class TimeConverterTest {

        [Test]
        public void TestConvertFromSeconds() {
            // Arrange
            var timeConverter = new TimeConverter(l => TimeSpan.FromSeconds(l), DAYS, HOURS, MINUTES, SECONDS);

            // Act
            var result = timeConverter.Convert(123456789);

            // Assert
            Assert.That(result, Is.EqualTo("1428d 21h 33m 9s"));
        }

        [Test]
        public void TestConvertFromMinutes() {
            // Arrange
            var timeConverter = new TimeConverter(l => TimeSpan.FromMinutes(l), DAYS, HOURS, MINUTES, SECONDS);

            // Act
            var result = timeConverter.Convert(2057613);

            // Assert
            Assert.That(result, Is.EqualTo("1428d 21h 33m"));
        }

        [Test]
        public void TestConvertWithZero() {
            // Arrange
            var timeConverter = new TimeConverter(l => TimeSpan.FromSeconds(l), DAYS, HOURS, MINUTES, SECONDS);

            // Act
            var result = timeConverter.Convert(0);

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }
    }
}
