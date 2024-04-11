using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Test.Helpers;
using Spg.AloMalo.DomainModel.Validators;
using Spg.AloMalo.Infrastructure;

namespace Spg.AloMalo.DomainModel.Test;

public class AlbumTests
{

    [Fact]
    public void Album_ShouldCreate_WhenEntitiesComplete()
    {
        using (PhotoContext db = DatabaseUtilities.CreateDb())
        {
            // Arrange
            Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
            Album newAlbum1 = new Album(
                "Test Album Homer", "Simpson Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            )
            .Validate();
            Album newAlbum2 = new Album(
                "Test Simpson Album 02", "Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            )
            .Validate();

            ModelValidator<Album> validator = new Validators.ModelValidator<Album>();
            bool isValid = validator.Validate(newAlbum1);


            newAlbum1.Name = "vieeeeeeeel zu langer name";

            // Act
            db.Albums.Add(newAlbum1);
            db.Albums.Add(newAlbum2);
            db.SaveChanges();

            // Assert
            Assert.Equal(2, db.Albums.Count());
        }
    }

    [Fact]
    public void Album_ShouldInsertValidPhotos_WhenListNotNull()
    {
        using (PhotoContext db = DatabaseUtilities.CreateDb())
        {
            // Arrange
            Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
            Album newAlbum = new Album(
                "Test Album 01", "Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            ).AddPhotos(new List<Photo>()
            {
                new Photo(
                    new Guid("11111111-1111-1111-1111-111111111111"),
                    "Test Photo 01",
                    "Beschreibung Test Photo 01...",
                    DateTime.Now,
                    ImageTypes.Png,
                    new Location(12, 17),
                    400, 800,
                    false,
                    newPhotographer),
            });

            // Act
            db.Albums.Add(newAlbum);
            db.SaveChanges();

            // Assert
            Assert.Equal(1, db.Albums.Count());
            Assert.Equal(1, db.Photos.Count());
            Assert.Single(db.Albums.First().AlbumPhotos);
        }
    }

    [Fact]
    public void Album_ShouldNotInsertValidPhotos_WhenListIsNull()
    {
        using (PhotoContext db = DatabaseUtilities.CreateDb())
        {
            // Arrange
            Photographer newPhotographer = DatabaseUtilities.GetSeedingPhotographers()[0];
            Album newAlbum = new Album(
                "Test Album 01", "Beschreibung...",
                true,
                newPhotographer,
                new TimeStampProvider()
            ).AddPhotos(
                null!
            );

            // Act
            db.Albums.Add(newAlbum);
            db.SaveChanges();

            // Assert
            Assert.Equal(1, db.Albums.Count());
            Assert.Empty(db.Photos);
            Assert.Empty(db.Albums.First().AlbumPhotos);
        }
    }
}