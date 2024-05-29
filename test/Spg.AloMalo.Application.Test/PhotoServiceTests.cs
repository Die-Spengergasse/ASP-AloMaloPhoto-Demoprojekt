using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Test.Helpers;
using Spg.AloMalo.Infrastructure;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.Repository.Repositories;

namespace Spg.AloMalo.Application.Test
{
    public class PhotoServiceTests
    {
        [Fact]
        public void OK_Test()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestIfInsertWorks()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                Assert.Equal(8, db.Photos.Count());
            }
        }

        [Fact]
        public void Should_Get4Photos_When_NameStartsWithB()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("name sw b"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(4, result.Count());
            }
        }

        [Fact]
        public void Should_Get0Photos_When_DateBetween2020And2022()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("creationtimestamp bw 2020.01.01 2022.12.31"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(0, result.Count());
            }
        }

        [Fact]
        public void Should_Get2Photos_When_NameContainsE()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("name ct e"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public void Should_Get5Photos_When_WidthGreaterThan200()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("width gt 200"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(5, result.Count());
            }
        }

        [Fact]
        public void Should_Get8Photos_When_WidthGreaterThanEqual200()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("width gte 200"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(8, result.Count());
            }
        }

        [Fact]
        public void Should_Get5Photos_When_WidthLowerThan500()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("width lt 500"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(5, result.Count());
            }
        }

        [Fact]
        public void Should_Get7Photos_When_WidthLowerThanEqual800()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                        new DomainModel.Queries.GetPhotosQuery("width lte 800"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(7, result.Count());
            }
        }
    }
}
