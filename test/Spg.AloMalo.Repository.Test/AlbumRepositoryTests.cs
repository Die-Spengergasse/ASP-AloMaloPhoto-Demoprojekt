namespace Spg.AloMalo.Repository.Test
{
    public class AlbumRepositoryTests
    {
        [Fact]
        public void OK_Test()
        {
            Assert.True(true);
        }

        //[Fact()]
        //public void ShouldReturnAllAlbums()
        //{
        //    // Arrange
        //    var albumMock = new Mock<PhotoContext>();
        //    albumMock
        //        .Setup<DbSet<Album>>(a => a.Albums)
        //        .ReturnsDbSet(DatabaseUtilities.GetSeedingAlbums());

        //    // Act
        //    AlbumRepository albumRepository = new AlbumRepository(albumMock.Object);
        //    var result = albumRepository.GetAll();

        //    // Assert
        //    Assert.Equal(4, result.Count());
        //}
    }
}
