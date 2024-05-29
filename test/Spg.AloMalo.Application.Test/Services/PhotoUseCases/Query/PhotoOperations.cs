using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.DomainModel.Dtos;
using Spg.AloMalo.DomainModel.Queries;

namespace Spg.AloMalo.Application.Test.Services.PhotoUseCases.Query {
    public class PhotoOperations {
        [Fact]
        public async void ContainsOperationOnNameShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void BeginsWithOperationOnNameShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo2.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void EndsWithOperationOnNameShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo2.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void ContainsOperationOnDescriptionShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void BeginsWithOperationOnDescriptionShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo1.ToDto(), filteredPhotos);
        }

        [Fact]
        public async void EndsWithOperationOnDescriptionShouldReturnCorrectPhoto() {
            var repo = new MockPhotoRepository();
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
            Assert.Contains(MockPhotoRepository.photo2.ToDto(), filteredPhotos);
        }
    }
}
