using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Test.Helpers;
using Spg.AloMalo.Infrastructure;
using Testcontainers.PostgreSql;

namespace Spg.AloMalo.DomainModel.Test
{
    public class PostgrePhotoTests : IAsyncLifetime
    {
        private readonly PostgreSqlContainer _db = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .Build();

        private PhotoContext CreateDb()
        {
            string connectionString = _db.GetConnectionString();

            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseNpgsql(connectionString);

            PhotoContext context = new PhotoContext(dbContextOptionsBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }

        public Task InitializeAsync()
        {
            return _db.StartAsync();
        }

        public Task DisposeAsync()
        {
            return _db.DisposeAsync().AsTask();
        }

        [Fact]
        public void ShouldCreateOnePhotographer_WhenParametersAreValid()
        {
            using (PhotoContext db = CreateDb())
            {
                // Arrange
                Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];

                // Act
                db.Photographers.Add(newPhotographer);
                db.SaveChanges();

                // Assert
                Assert.Single(db.Photographers);
            }
        }
    }
}
