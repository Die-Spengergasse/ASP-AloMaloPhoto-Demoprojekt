using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Repositories;
using Spg.AloMalo.Repository.Test.Helpers;

namespace Spg.AloMalo.Repository.Test
{
    public class AlbumRepositoryTests
    {
        [Fact()]
        public void ShouldReturnAllAlbums()
        {
            //// Arrange
            //var albumMock = new Mock<PhotoContext>();
            //albumMock
            //    .Setup<DbSet<Album>>(a => a.Albums)
            //    .ReturnsDbSet(DatabaseUtilities.GetSeedingAlbums());

            //// Act
            //AlbumRepository albumRepository = new AlbumRepository(albumMock.Object);
            //var result = albumRepository.GetAll();

            //// Assert
            //Assert.Equal(4, result.Count());
        }
    }
}
