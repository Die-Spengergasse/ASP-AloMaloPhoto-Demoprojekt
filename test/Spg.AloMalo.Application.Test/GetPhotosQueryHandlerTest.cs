using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.Repository.Builder;

namespace Spg.AloMalo.Application.Test
{
    internal class GetPhotosQueryHandlerTest
    {
        private readonly GetPhotosQueryHandler _handler;
        private readonly List<Photo> _photos;
        private readonly Mock<IReadOnlyPhotoRepository> _photoRepositoryMock;

        public GetPhotosQueryHandlerTest()
        {
            _photos = new List<Photo>
            {
                new Photo(Guid.Empty, "Alice5", "Sunset at Beach!", DateTime.Now, ImageTypes.Png, new Location(1, 2), 1, 10, false, null!),
                new Photo(Guid.Empty, "Bob3", "Mountain Hike", DateTime.Now, ImageTypes.Png, new Location(1, 2), 2, 20, false, null!),
                new Photo(Guid.Empty, "Charlie", "City Skyline?", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 30, false, null!),
                new Photo(Guid.Empty, "Dave", "Funny Meme", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 45, false, null!),
                new Photo(Guid.Empty, "Eve", "Celebration", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 13, false, null!)
            };

            var photoFilterBuilder = new PhotoFilterBuilder(_photos.AsQueryable());

            _photoRepositoryMock = new Mock<IReadOnlyPhotoRepository>();
            _photoRepositoryMock.Setup(repo => repo.FilterBuilder).Returns(photoFilterBuilder);

            _handler = new GetPhotosQueryHandler(_photoRepositoryMock.Object);
        }

        [Theory]
        [InlineData("Name equals Alice", 1)]
        [InlineData("Name contains Bob", 1)]
        [InlineData("Name startswith Charlie", 1)]
        [InlineData("Name endswith ve", 2)]
        [InlineData("Name containsdigits", 2)]
        [InlineData("Height greaterthan 15", 3)]
        [InlineData("Height greaterthanequal 20", 3)]
        [InlineData("Height lt 25", 3)]
        [InlineData("Height lte 20", 3)]
        [InlineData("Description containsspecialchars", 2)]
        public async Task Handle_ShouldApplyCorrectFilters(string filter, int expectedCount)
        {
           
            var request = new GetPhotosQueryModel(new GetPhotosQuery(filter, string.Empty));

            
            var result = await _handler.Handle(request, CancellationToken.None);

            
            Assert.Equal(expectedCount, result.Count);
        }
    }
}
