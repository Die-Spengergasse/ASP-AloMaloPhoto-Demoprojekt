using System;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Test.Helpers;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.Repository.Repositories;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.DomainModel.Dtos;

namespace Spg.AloMalo.DomainModel.Test
{
	public class FilterTest
	{
        [Fact]
        public void NameContainsFilterTest()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                Photographer newPhotographer1 = DatabaseUtilities.GetSeedingPhotographers()[0];
                Photo newPhoto1 = new Photo(
                    new Guid("11111111-1111-1111-1111-111111111111"),
                    "Test Photo 01",
                    "Beschreibung Test Photo 01...",
                    DateTime.Now,
                    ImageTypes.Png,
                    new Location(12, 17),
                    400, 800,
                    false,
                    newPhotographer1
                ).AddAlbums(new Album(
                    "Test Album 01", "Beschreibung...",
                    true,
                    newPhotographer1,
                    new TimeStampProvider()
                ));
                Photographer newPhotographer2 = DatabaseUtilities.GetSeedingPhotographers()[1];
                Photo newPhoto2 = new Photo(
                    new Guid("22222222-2222-2222-2222-222222222222"),
                    "Test Photo blabla 02",
                    "Beschreibung Test Photo 01...",
                    DateTime.Now,
                    ImageTypes.Png,
                    new Location(12, 17),
                    400, 800,
                    false,
                    newPhotographer2
                ).AddAlbums(new Album(
                    "Test Album 02", "Beschreibung...",
                    true,
                    newPhotographer2,
                    new TimeStampProvider()
                ));

                // Act
                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();

                GetPhotosQueryModel query = new GetPhotosQueryModel(new Queries.GetPhotosQuery("name ct blabla", ""));
                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos),new PhotoUpdateBuilder(db)));

                var result = handler.Handle(query,CancellationToken.None).Result;

                Assert.Equal(1, result.Count());
                    
            }
        }



        [Fact]
        public void NameEndwithFilterTest()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                // Arrange
                Photographer newPhotographer1 = DatabaseUtilities.GetSeedingPhotographers()[0];
                Photo newPhoto1 = new Photo(
                    new Guid("11111111-1111-1111-1111-111111111111"),
                    "Test Photo 01",
                    "Beschreibung Test Photo 01...",
                    DateTime.Now,
                    ImageTypes.Png,
                    new Location(12, 17),
                    400, 800,
                    false,
                    newPhotographer1
                ).AddAlbums(new Album(
                    "Test Album 01", "Beschreibung...",
                    true,
                    newPhotographer1,
                    new TimeStampProvider()
                ));
                Photographer newPhotographer2 = DatabaseUtilities.GetSeedingPhotographers()[1];
                Photo newPhoto2 = new Photo(
                    new Guid("22222222-2222-2222-2222-222222222222"),
                    "Test Photo 02",
                    "Beschreibung Test Photo 01...",
                    DateTime.Now,
                    ImageTypes.Png,
                    new Location(12, 17),
                    400, 800,
                    false,
                    newPhotographer2
                ).AddAlbums(new Album(
                    "Test Album 02", "Beschreibung...",
                    true,
                    newPhotographer2,
                    new TimeStampProvider()
                ));

                // Act
                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();

                GetPhotosQueryModel query = new GetPhotosQueryModel(new Queries.GetPhotosQuery("name ew 02", ""));
                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));

                var result = handler.Handle(query, CancellationToken.None).Result;

                Assert.Equal(1, result.Count());

            }
        }

    }
}

