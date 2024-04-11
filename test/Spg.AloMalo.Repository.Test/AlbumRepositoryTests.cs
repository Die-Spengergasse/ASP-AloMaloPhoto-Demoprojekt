using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;
using Spg.AloMalo.Repository.Test.Helpers;

namespace Spg.AloMalo.Repository.Test
{
    public class AlbumRepositoryTests
    {
        [Fact]
        public void ShouldCreateOne()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                db.Albums.AddRange(DatabaseUtilities.GetSeedingAlbums(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();

                Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
                AlbumRepository utt = new AlbumRepository(db, new AlbumFilterBuilder(db.Albums));

                // Act
                utt.Create(new Album(
                    "Created Album Album", "Created Album Album Description",
                    true,
                    newPhotographer,
                    new TimeStampProvider()
                ));

                // Assert
                Assert.Equal(4, db.Albums.Count());
            }
        }

        [Fact]
        public void ShouldDeleteOne()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                db.Albums.AddRange(DatabaseUtilities.GetSeedingAlbums(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();

                Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
                AlbumRepository utt = new AlbumRepository(db, new AlbumFilterBuilder(db.Albums));

                // Act
                utt.Delete(new PhotoId(2));

                // Assert
                Assert.Equal(2, db.Albums.Count());
            }
        }
    }
}
