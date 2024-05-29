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
        public void DatabaseInsertTest()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                Assert.Equal(8, db.Photos.Count());
            }
        }

        [Fact]
        public void FilterByNameStartingWithB()
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
        public void FilterByCreationDateBetween2020And2022()
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
        public void FilterByNameContainingE()
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
        public void FilterByWidthGreaterThan200()
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
        public void FilterByNameNotEqualToX()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("name nq x"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(0, result.Count());
            }
        }

        [Fact]
        public void FilterByHeightInList()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("height in 100,200"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(2, result.Count());
            }
        }

        [Fact]
        public void FilterByHeightNotInList()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("height ni 100,200"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(6, result.Count());
            }
        }

        [Fact]
        public void FilterByDescriptionRegexMatch()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("description rx ^A.*$"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(3, result.Count());
            }
        }

        [Fact]
        public void FilterByDescriptionContainsDigits()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("description cd"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(4, result.Count());
            }
        }

        [Fact]
        public void FilterByCreationDateRange()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("creationtimestamp dr 2020.01.01 2022.12.31"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(0, result.Count());
            }
        }

        [Fact]
        public void FilterByNameEndsWithN()
        {
            using (PhotoContext db = DatabaseUtilities.CreateDb())
            {
                db.Photos.AddRange(DatabaseUtilities.GetSeedingPhotos(DatabaseUtilities.GetSeedingPhotographers()));
                db.SaveChanges();
                GetPhotosQueryModel model = new GetPhotosQueryModel(
                    new DomainModel.Queries.GetPhotosQuery("name ew n"));

                GetPhotosQueryHandler handler = new GetPhotosQueryHandler(
                    new PhotoRepository(db, new PhotoFilterBuilder(db.Photos), new PhotoUpdateBuilder(db)));
                IQueryable<PhotoDto> result = handler.Handle(model, CancellationToken.None).Result;

                Assert.Equal(3, result.Count());
            }
        }
    }
}
