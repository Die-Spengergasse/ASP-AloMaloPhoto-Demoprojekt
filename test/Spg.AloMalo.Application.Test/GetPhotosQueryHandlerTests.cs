using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.Repository.Builder;

namespace Spg.AloMalo.Application.Test
{
    public class GetPhotosQueryHandlerTests
    {
        private readonly GetPhotosQueryHandler _handler;
        private readonly List<Photo> _photos;
        private readonly Mock<IReadOnlyPhotoRepository> _photoRepositoryMock;

        public GetPhotosQueryHandlerTests()
        {
            _photos = new List<Photo>
            {
                new Photo(Guid.Empty, "Smith123", "Photo!", DateTime.Now, ImageTypes.Png, new Location(1, 2), 1, 10, false, null!),
                new Photo(Guid.Empty, "Jones456", "AnotherPhoto?", DateTime.Now, ImageTypes.Png, new Location(1, 2), 2, 20, false, null!),
                new Photo(Guid.Empty, "Brown789", "ThirdPhoto#", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 30, false, null!),
                new Photo(Guid.Empty, "Meme23", "Das ist ein* cooles Meme", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 45, false, null!),
                new Photo(Guid.Empty, "ala&", "juhu", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 13, false, null!)
            };

            var photoFilterBuilder = new PhotoFilterBuilder(_photos.AsQueryable());

            _photoRepositoryMock = new Mock<IReadOnlyPhotoRepository>();

            // Setup the repository mock to return the mocked FilterBuilder
            _photoRepositoryMock.Setup(repo => repo.FilterBuilder).Returns(photoFilterBuilder);

            _handler = new GetPhotosQueryHandler(_photoRepositoryMock.Object);
        }

        [Theory]
        [InlineData("Name equals Smith123", 1)]
        [InlineData("Name contains Smith", 1)]
        [InlineData("Name startswith Smith", 1)]
        [InlineData("Name endswith 123", 1)]
        [InlineData("Name containsdigits", 4)]
        [InlineData("Height greaterthan 15", 3)]
        [InlineData("Height greaterthanequal 20", 3)]
        [InlineData("Height lt 25", 3)]
        [InlineData("Height lte 20", 3)]
        [InlineData("Description containsspecialchars", 4)]
        public async Task Handle_ShouldApplyCorrectFilters(string filter, int expectedCount)
        {
            // Arrange
            var request = new GetPhotosQueryModel(new GetPhotosQuery(filter, string.Empty));

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.Equal(expectedCount, result.Count);
        }

    }
}