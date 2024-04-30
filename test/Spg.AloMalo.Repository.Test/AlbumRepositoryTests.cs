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
                utt.Delete<int, AlbumId>(new AlbumId(2));

                // Assert
                Assert.Equal(2, db.Albums.Count());
            }
        }

        [Fact]
        public void ShouldFindByPrimaryKey()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                var albums = DatabaseUtilities.GetSeedingAlbums(DatabaseUtilities.GetSeedingPhotographers());
                var photos = DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers());
                db.Albums.AddRange(albums);
                db.Photos.AddRange(photos);
                db.AlbumPhotos.AddRange(DatabaseUtilities.GetSeedingAlbumPhotos(albums, photos));
                db.SaveChanges();

                Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
                AlbumRepository utt = new AlbumRepository(db, new AlbumFilterBuilder(db.Albums));

                // Act
                Album? actual = utt.GetByPK<AlbumId, AlbumPhoto>(new AlbumId(2));

                // Assert
                Assert.NotNull(actual);
                Assert.Equal("Menschen", actual.Name);
                Assert.Equal("daefsrgdtfhjk sdfrgth kjh sd d", actual.Description);
            }
        }

        [Fact]
        public void ShouldFindByPrimaryKeyAndIncludeAllPhotos()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                var albums = DatabaseUtilities.GetSeedingAlbums(DatabaseUtilities.GetSeedingPhotographers());
                var photos = DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers());
                db.Albums.AddRange(albums);
                db.Photos.AddRange(photos);
                db.AlbumPhotos.AddRange(DatabaseUtilities.GetSeedingAlbumPhotos(albums, photos));
                db.SaveChanges();

                Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
                AlbumRepository utt = new AlbumRepository(db, new AlbumFilterBuilder(db.Albums));

                // Act
                Album? actual = utt.GetByPK(new AlbumId(2),
                    includeCollection: a => a.AlbumPhotos);

                // Assert
                Assert.NotNull(actual);
                Assert.Equal("Menschen", actual.Name);
                Assert.Equal("daefsrgdtfhjk sdfrgth kjh sd d", actual.Description);
                Assert.Equal(3, actual.AlbumPhotos.Count);
                Assert.NotNull(actual.AlbumPhotos.First().PhotoNavigation);
            }
        }
    }
}
