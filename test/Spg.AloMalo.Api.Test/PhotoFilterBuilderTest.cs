using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Infrastructure;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Spg.AloMalo.Api.Test.Helpers;
using Spg.AloMalo.DomainModel;
using Spg.AloMalo.Repository.Repositories;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query.Tests
{
    public class PhotoFilterBuilderTests
    {
        private void SeedDatabase(PhotoContext context)
        {
            var photos = new List<Photo> {
                
            };
            context.Photos.AddRange(photos);
            context.SaveChanges();
        }
        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithNameContainsT()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("name ct T", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count);
            }
        }
       
        

        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithHeightGreaterThan100()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Height gt 100", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count);
            }
        }
            [Fact]
            public void GetPhotosQueryHandler_ShouldReturnPhotos_WithHeightLowerThan1000()
            {
                using (PhotoContext db = DatabaseUtilities.CreateDb())
                {
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

                    db.Photos.Add(newPhoto1);
                    db.Photos.Add(newPhoto2);
                    db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Height lt 1000", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                    Assert.Equal(2, result.Count);
                }
            }
        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithHeightPlusMinus100From750()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Height pm100 750", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count);
            }
        }
        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithDescriptionBeginsWithBesch()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Description bw Besch", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count);
            }
        }
        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithNameEqualsTest()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                Photographer newPhotographer1 = DatabaseUtilities.GetSeedingPhotographers()[0];
                Photo newPhoto1 = new Photo(
                    new Guid("11111111-1111-1111-1111-111111111111"),
                    "Test",
                    "Beschreibung Test Photo 1...",
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Name eq Test", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count());
            }
        }
        
   
        [Fact]
        public void GetPhotosQueryHandler_ShouldReturnPhotos_WithNamesEndingWith02()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
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
                    "Test Photo blabla 01",
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

                db.Photos.Add(newPhoto1);
                db.Photos.Add(newPhoto2);
                db.SaveChanges();
                var handler = new GetPhotosQueryHandler(new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                var query = new GetPhotosQueryModel(new DomainModel.Queries.GetPhotosQuery("Name ew 01", ""));
                var builder = new PhotoFilterBuilder(db.Photos);
                var result = handler.Handle(query, CancellationToken.None).Result;
                Console.WriteLine(result);
                Assert.Equal(2, result.Count());
            }
        }
    }
    }

