using TourPlanner.Data;
using TourPlanner.Model;

namespace TourPlanner.Test {

    internal class EnumLikeConverterTest {

        [Test]
        public void TestConvertToProvider() {
            // Arrange
            var converter = new EnumLikeValueConverter<Difficulty, int>(Difficulty.ALL);

            // Act
            var result = converter.ConvertToProvider(Difficulty.EASY);

            // Assert
            Assert.That(result, Is.EqualTo(Difficulty.EASY.Id));
        }

        [Test]
        public void TestConvertFromProvider() {
            // Arrange
            var converter = new EnumLikeValueConverter<Difficulty, int>(Difficulty.ALL);

            // Act
            var result = converter.ConvertFromProvider(Difficulty.EASY.Id);

            // Assert
            Assert.That(result, Is.EqualTo(Difficulty.EASY));
        }
    }
}
