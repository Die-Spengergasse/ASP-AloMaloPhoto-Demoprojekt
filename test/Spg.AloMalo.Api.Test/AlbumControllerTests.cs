using Spg.AloMalo.Api.Test.Helpers;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.MockMvc;
using System.Net;

namespace Spg.AloMalo.Api.Test
{
    public class AlbumControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;
        // Dependency injection from IClassFixture
        public AlbumControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllVehiclesReturnsOkTest()
        {
            _factory.InitializeDatabase(db =>
            {
                var albums = DatabaseUtilities.GetSeedingAlbums(DatabaseUtilities.GetSeedingPhotographers());
                db.Albums.AddRange(albums);
                db.SaveChanges();

                var a = db.Albums;
            });
            var (statusCode, albums) = await _factory.GetHttpContent<List<AlbumDto>>("/api/album/ok");
            Assert.Equal(HttpStatusCode.OK, statusCode);
        }
    }
}
