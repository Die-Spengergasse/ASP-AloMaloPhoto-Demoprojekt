using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.Repository.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Spg.AloMalo.Repository.Test
{
    public class PhotoQueryHandlerTest
    {
        private readonly GetPhotosQueryHandler _handler;
        private readonly List<Photo> _photos;

        public PhotoQueryHandlerTest()
        {
            _photos = new List<Photo>
            {
                new Photo(Guid.NewGuid(), "Alice42", "Beautiful Landscape", DateTime.Now, ImageTypes.Jpg, new Location(3, 4), 5, 50, false, null!),
                new Photo(Guid.NewGuid(), "Bob007", "Amazing Sunset!", DateTime.Now, ImageTypes.Png, new Location(5, 6), 10, 60, true, null!),
                new Photo(Guid.NewGuid(), "Charlie123", "Wonderful Beach", DateTime.Now, ImageTypes.Nef, new Location(7, 8), 15, 70, false, null!)
            };

            var photoRepositoryMock = new Mock<IReadOnlyPhotoRepository>();
            photoRepositoryMock.Setup(repo => repo.FilterBuilder).Returns(new PhotoFilterBuilder(_photos.AsQueryable()));

            _handler = new GetPhotosQueryHandler(photoRepositoryMock.Object);
        }

        [Theory]
        [InlineData("name equals Alice42", 1)]
        [InlineData("name contains Bob", 1)]
        [InlineData("name startswith Charlie", 1)]
        [InlineData("name endswith 123", 1)]
        [InlineData("name containsdigits", 3)]
        [InlineData("height greaterthan 51", 2)]
        [InlineData("height greaterthanequal 70", 1)]
        [InlineData("description contains Beach", 1)]
        [InlineData("description contains Sunset", 1)]
        [InlineData("description contains Landscape", 1)]
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
