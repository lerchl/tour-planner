using Moq;
using TourPlanner.Data.Repository;
using TourPlanner.Logic.Service;
using TourPlanner.Logic.Validation;
using TourPlanner.Model;

namespace TourPlanner.Test {

    internal class CrudServiceTest {

        [Test]
        public void TestGetAll() {
            // Arrange
            var tour = new Tour();
            var tourLog = new TourLog();
            var list = new List<Entity>() { tour, tourLog };

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.GetAll()).Returns(list);

            var mockValidator = new Mock<IValidator<Entity>>();
            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
            Assert.That(result, Has.Member(tour));
            Assert.That(result, Has.Member(tourLog));
        }

        [Test]
        public void TestGetByIdExists() {
            // Arrange
            var tour = new Tour() { Id = Guid.NewGuid() };

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.GetById(tour.Id)).Returns(tour);

            var mockValidator = new Mock<IValidator<Entity>>();
            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            var result = service.GetById(tour.GetGuid());

            // Assert
            Assert.That(result, Is.EqualTo(tour));
        }

        [Test]
        public void TestGetByIdDoesNotExist() {
            // Arrange
            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns((Entity?) null);

            var mockValidator = new Mock<IValidator<Entity>>();
            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            var result = service.GetById(Guid.NewGuid());

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test]
        public void TestAddValid() {
            // Arrange
            var tour = new Tour();

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.Add(tour)).Returns(tour);

            var mockValidator = new Mock<IValidator<Entity>>();
            mockValidator.Setup(x => x.ValidateSave(tour)).Returns(new ValidationResult());

            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            var result = service.Add(tour);

            // Assert
            Assert.That(result, Is.EqualTo(tour));
        }

        [Test]
        public void TestAddInvalid() {
            // Arrange
            var tour = new Tour();
            var validationResult = new ValidationResult();
            validationResult.AddMessage(new ValidationMessage(ValidationMessageStatus.Error, ""));

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.Add(tour)).Returns(tour);

            var mockValidator = new Mock<IValidator<Entity>>();
            mockValidator.Setup(x => x.ValidateSave(tour)).Returns(validationResult);

            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            // Assert
            Assert.Throws<ValidationException>(() => service.Add(tour));
        }

        [Test]
        public void TestUpdateValid() {
            // Arrange
            var tour = new Tour();

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.Update(tour)).Returns(tour);

            var mockValidator = new Mock<IValidator<Entity>>();
            mockValidator.Setup(x => x.ValidateSave(tour)).Returns(new ValidationResult());

            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            var result = service.Update(tour);

            // Assert
            Assert.That(result, Is.EqualTo(tour));
        }

        [Test]
        public void TestUpdateInvalid() {
            // Arrange
            var tour = new Tour();
            var validationResult = new ValidationResult();
            validationResult.AddMessage(new ValidationMessage(ValidationMessageStatus.Error, ""));

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.Update(tour)).Returns(tour);

            var mockValidator = new Mock<IValidator<Entity>>();
            mockValidator.Setup(x => x.ValidateSave(tour)).Returns(validationResult);

            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            // Assert
            Assert.Throws<ValidationException>(() => service.Update(tour));
        }

        [Test]
        public void TestRemove() {
            // Arrange
            var tour = new Tour();

            var mockRepository = new Mock<ICrudRepository<Entity>>();
            mockRepository.Setup(x => x.Remove(tour));

            var mockValidator = new Mock<IValidator<Entity>>();
            var service = new CrudService<Entity, ICrudRepository<Entity>, IValidator<Entity>>(mockRepository.Object, mockValidator.Object);

            // Act
            service.Remove(tour);

            // Assert
            mockRepository.Verify(x => x.Remove(tour), Times.Once);
        }
    }
}
