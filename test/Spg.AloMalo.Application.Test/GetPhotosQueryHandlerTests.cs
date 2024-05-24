using Moq;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Commands;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Test
{
    public class GetPhotosQueryHandlerTests
    {
        private readonly GetPhotosQueryHandler _handler;
        private readonly List<Photo> _photos;

        public GetPhotosQueryHandlerTests()
        {
            _photos = new List<Photo>
        {
            new Photo(Guid.Empty, "Smith123", "Photo!", DateTime.Now, ImageTypes.Png, new Location(1, 2), 1, 10, false, null!),
            new Photo(Guid.Empty, "Jones456", "AnotherPhoto?", DateTime.Now, ImageTypes.Png, new Location(1, 2), 2, 20, false, null!),
            new Photo(Guid.Empty, "Brown789", "ThirdPhoto#", DateTime.Now, ImageTypes.Png, new Location(1, 2), 3, 30, false, null!)
        };
            using (var db = new PhotoContext())
            {
                _handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(_photos.AsQueryable()), new PhotoUpdateBuilder(db)));
            }
        }

        [Theory]
        [InlineData("Name equals Smith123", 1)]
        [InlineData("Name contains Smith", 1)]
        [InlineData("Name startswith Smith", 1)]
        [InlineData("Name endswith 123", 1)]
        [InlineData("Name containsdigits", 3)]
        [InlineData("Height greaterthan 15", 2)]
        [InlineData("Height greaterthanequal 20", 2)]
        [InlineData("Height lt 25", 2)]
        [InlineData("Height lte 20", 2)]
        [InlineData("Description containsspecialchars", 3)]
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
