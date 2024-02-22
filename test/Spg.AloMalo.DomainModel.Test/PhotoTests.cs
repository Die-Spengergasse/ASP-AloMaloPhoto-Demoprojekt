using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Test.Helpers;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.DomainModel.Test;

public class PhotoTests : IClassFixture<DatabaseFixture>
{
    [Fact]
    public void OK_Test()
    {
        Assert.True(true);
    }


    //private readonly DatabaseFixture _databaseFixture;

    //public PhotoTests(DatabaseFixture databaseFixture)
    //{
    //    _databaseFixture = databaseFixture;
    //}

    [Fact]
    public void Photo_ShouldInsertValidAlbums_WhenListNotNull()
    {
        using (PhotoContext db = DatabaseUtilities.CreateDb())
        {

            // Arrange
            Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
            Photo newPhoto = new Photo(
                "Test Photo 01",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                Orientations.Landscape,
                false,
                newPhotographer
            ).AddAlbums(new Album(
                "Test Album 01", "Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            ));

            newPhoto.Name = "asdasdasdasdasdaasdsdadas";

            // Act
            db.Photos.Add(newPhoto);
            db.SaveChanges();

            // Assert
            Assert.Equal(1, db.Albums.Count());
            Assert.Equal(1, db.Photos.Count());
            Assert.Single(db.Albums.First().AlbumPhotos);
        }
    }

    [Fact]
    public void Photo_ShouldNOTInsertValidAlbums_WhenListIsNull()
    {
        using (PhotoContext db = DatabaseUtilities.CreateDb())
        {
            // Arrange
            Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
            Photo newPhoto = new Photo(
                "Test Photo 01",
                "Beschreibung Test Photo 01...",
                DateTime.Now,
                ImageTypes.Png,
                new Location(12, 17),
                400, 800,
                Orientations.Landscape,
                false,
                newPhotographer
            ).AddAlbums(
                null!
            );

            // Act
            db.Photos.Add(newPhoto);
            db.SaveChanges();

            // Assert
            Assert.Equal(0, db.Albums.Count());
        }
    }
}