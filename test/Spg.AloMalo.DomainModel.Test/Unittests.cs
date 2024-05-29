using Spg.AloMalo.DomainModel.Model;
using Spg.AloMalo.Application.Services.PhotoUseCases.Query;
using Spg.AloMalo.Repository.Builder;
using Spg.AloMalo.DomainModel.Queries;
using Spg.AloMalo.DomainModel.Interfaces.Repositories;
using Moq;

namespace Spg.AloMalo.Application.Test
{
    public class Unittests
    {
        private readonly GetPhotosQueryHandler _handler;
        private readonly List<Photo> _photos;
        private readonly Mock<IReadOnlyPhotoRepository> _photoRepositoryMock;

        public Unittests()
        {

            _photos = new List<Photo>
            {
                new Photo(Guid.Empty, "Mountains", "Photo-of-a-Mountain", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 10, 10, false, null!),
                new Photo(Guid.Empty, "Big-Apple-Pie", "A-delicious-Apple-Pie", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 2, 5, false, null!),
                new Photo(Guid.Empty, "Small-Apple-Pie", "A-delicious-Apple-Pie", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 7, 9, false, null!),
                new Photo(Guid.Empty, "Big-Banana-Pie", "A-delicious-Banana-Pie", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 5, 4, false, null!),
                new Photo(Guid.Empty, "Small-Banana-Pie", "A-delicious-Banana-Pie", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 9, 7, false, null!),
                new Photo(Guid.Empty, "Red-Car", "A-beautiful-red-Car", DateTime.Now, ImageTypes.Png, new Location(100, 20), 12, 8, false, null!),
                new Photo(Guid.Empty, "Blue-Car", "A-beautiful-blue-Car", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 6, 5, false, null!),
                new Photo(Guid.Empty, "1BHIF-Klassenfoto", "1BHIF-Picture", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 3, 7, false, null!),
                new Photo(Guid.Empty, "2BHIF-Klassenfoto", "2BHIF-Picture", DateTime.Now, ImageTypes.Png, new Location(100, 20), 7, 2, false, null!),
                new Photo(Guid.Empty, "3BHIF-Klassenfoto", "3BHIF-Picture", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 2, 6, false, null!),
                new Photo(Guid.Empty, "4BHIF-Klassenfoto", "4BHIF-Picture", DateTime.Now, ImageTypes.Jpg, new Location(100, 20), 10, 12, false, null!),
                new Photo(Guid.Empty, "5BHIF-Klassenfoto", "5BHIF-Picture", DateTime.Now, ImageTypes.Png, new Location(100, 20), 7, 3, false, null!),
            };

            var photoFilterBuilder = new PhotoFilterBuilder(_photos.AsQueryable());

            _photoRepositoryMock = new Mock<IReadOnlyPhotoRepository>();

            _photoRepositoryMock.Setup(repo => repo.FilterBuilder).Returns(photoFilterBuilder);

            _handler = new GetPhotosQueryHandler(_photoRepositoryMock.Object);
        }

        [Fact]
        public async Task NameEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Name eq Mountains", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public async Task NameBeginsWithTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Name bw Big", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }

        //Name
        [Fact]
        public async Task NameContainsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Description ct Apple", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }


        [Fact]
        public async Task NameEndsWithTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Name ew BHIF-Klassenfoto", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task DescriptionEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Description eq A-delicious-Apple-Pie", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DescriptionBeginsWithTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Description bw A-delicious", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(4, result.Count);
        }

        //Description
        [Fact]
        public async Task DescriptionContainsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Description ct beautiful", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task DescriptionEndsWithTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Description ew Car", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }

        //Date (hier nur contains weil alles andere keinen Sinn macht)
        [Fact]
        public async Task DateContainsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Date ct 2024", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Console.WriteLine(result);
            Assert.Equal(12, result.Count);
        }

        //Width
        [Fact]
        public async Task WidthLessThanTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Width lt 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(5, result.Count);
        }

        [Fact]
        public async Task WidthLessThanEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Width lte 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(8, result.Count);
        }

        [Fact]
        public async Task WidthEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Width eq 10", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task WidthGreaterThanEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Width gte 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(7, result.Count);
        }

        [Fact]
        public async Task WidthGreaterThanTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Width gt 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(4, result.Count);
        }

        //Height
        [Fact]
        public async Task HeightLessThanTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Height lt 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(6, result.Count);
        }

        [Fact]
        public async Task HeightLessThanEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Height lte 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(8, result.Count);
        }

        [Fact]
        public async Task HeightEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Height eq 10", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Single(result);
        }

        [Fact]
        public async Task HeightGreaterThanEqualsTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Height lt 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(6, result.Count);
        }

        [Fact]
        public async Task HeightGreaterThanTest()
        {
            var query = new GetPhotosQueryModel(new GetPhotosQuery("Height gt 7", null!));
            var result = await _handler.Handle(query, CancellationToken.None);
            Assert.Equal(4, result.Count);
        }
    }
}