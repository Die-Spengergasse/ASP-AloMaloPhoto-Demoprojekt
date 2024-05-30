using Spg.AloMalo.DomainModel.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Spg.AloMalo.Application.Services.PhotoUseCases.Query
{
    public class PhotoOperations
    {
        [Fact]
        public async void ContainsOperationOnNameShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Name ct ark",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void BeginsWithOperationOnNameShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Name bw Sta",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo2.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void EndsWithOperationOnNameShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Name ew dt",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo2.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void ContainsOperationOnDescriptionShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Description ct öner",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void BeginsWithOperationOnDescriptionShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Description bw Ein",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void EndsWithOperationOnDescriptionShouldReturnCorrectPhoto()
        {
            var repo = new MockPhoto();
            var getHandler = new GetPhotosQueryHandler(repo);

            var filteredPhotos = await getHandler.Handle(
                new GetPhotosQueryModel(
                    new GetPhotosQuery(
                        "Description ew Stadt",
                        string.Empty
                    )
                ),
            CancellationToken.None);

            Assert.Single(filteredPhotos);
            Assert.Contains(MockPhoto.photo2.ToDto(), filteredPhotos);
        }
    }
}
