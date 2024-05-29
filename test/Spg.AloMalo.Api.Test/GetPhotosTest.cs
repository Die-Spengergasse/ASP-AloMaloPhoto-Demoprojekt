using NUnit.Framework;
using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

public class GetPhotosTest
{
    [TestFixture]
    public class PhotoFilterTests
    {
        private Mock<IPhotoFilterBuilder> _mockFilterBuilder;
        private List<Photo> _photos;
        private Mock<IReadOnlyPhotoRepository> _photoRepositoryMock;
        private GetPhotosQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _photos = new List<Photo>
            {
                new Photo(Guid.NewGuid(), "Smith123", "Photo!", DateTime.Now, ImageTypes.Png, new Location(1, 2), 1, 10, false, null!),
                new Photo(Guid.NewGuid(), "Jones456", "AnotherPhoto?", DateTime.Now, ImageTypes.Png, new Location(1, 2), 2, 20, false, null!),
                new Photo(Guid.NewGuid(), "Brown789", "ThirdPhoto#", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 30, false, null!),
                new Photo(Guid.NewGuid(), "Meme23", "Das ist ein* cooles Meme", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 45, false, null!),
                new Photo(Guid.NewGuid(), "ala&", "juhu", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 13, false, null!)
            };

            _mockFilterBuilder = new Mock<IPhotoFilterBuilder>();
            _photoRepositoryMock = new Mock<IReadOnlyPhotoRepository>();
            _photoRepositoryMock.Setup(repo => repo.FilterBuilder).Returns(_mockFilterBuilder.Object);

            _handler = new GetPhotosQueryHandler(_photoRepositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldApplyCorrectFilters()
        {
            // Arrange
            string filter = "Name equals Smith123";
            var request = new GetPhotosQueryModel(new GetPhotosQuery(filter, string.Empty));
            _mockFilterBuilder.Setup(x => x.ApplyEqualsFilter(It.IsAny<string>(), It.IsAny<string>()))
                              .Returns(_mockFilterBuilder.Object); // Simulating the chainable methods

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            _mockFilterBuilder.Verify(x => x.ApplyEqualsFilter("Name", "Smith123"), Times.Once);
        }

    }
}
