using System.Linq.Expressions;
using Spg.AloMalo.DomainModel.Filter;
using Spg.AloMalo.DomainModel.Model;

namespace Spg.AloMalo.Tests.Filters
{
    public class FilterTests
    {
        private IQueryable<Photo> _photos;

        public FilterTests()
        {
            var photographer = new Photographer(
                Guid.NewGuid(),
                "John",
                "Doe",
                new Address("123 Main St", "City", "12345", "Country"),
                new PhoneNumber(1, 123, "4567890"),
                new PhoneNumber(1, 987, "6543210"),
                new List<EMail> { new EMail("john.doe@example.com") },
                new EMail("johndoe")
            );

            _photos = new[]
            {
                new Photo(Guid.NewGuid(), "Photo 1", "Description 1", DateTime.Now, ImageTypes.Jpg, new Location(0, 0), 800, 600, false, photographer),
                new Photo(Guid.NewGuid(), "Photo 2", "Description 2", DateTime.Now, ImageTypes.Png, new Location(0, 0), 1200, 900, true, photographer),
                new Photo(Guid.NewGuid(), "Photo 3", "Description 3", DateTime.Now, ImageTypes.Jpg, new Location(0, 0), 1600, 1200, false, photographer),
                new Photo(Guid.NewGuid(), "Photo 4", "Description 4", DateTime.Now, ImageTypes.Png, new Location(0, 0), 2000, 1500, true, photographer)
            }.AsQueryable();
        }

        [Fact]
        public void EqualsFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, Guid>> propertySelector = p => p.Guid;
            var filter = new EqualsFilter<Photo, Guid>();

            var result = filter.Apply(propertySelector, _photos.First().Guid.ToString()).Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void StartsWithFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, string>> propertySelector = p => p.Name;
            var filter = new StartsWithFilter<Photo>();

            var result = filter.Apply(propertySelector, "Photo").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void EndsWithFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, string>> propertySelector = p => p.Description;
            var filter = new EndsWithFilter<Photo>();

            var result = filter.Apply(propertySelector, "n 1").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void ContainsFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, string>> propertySelector = p => p.Description;
            var filter = new ContainsFilter<Photo>();

            var result = filter.Apply(propertySelector, "tion").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void RegexFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, string>> propertySelector = p => p.Name;
            var filter = new RegexFilter<Photo>();

            var result = filter.Apply(propertySelector, "^Photo \\d$").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void ContainsDigitsFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, string>> propertySelector = p => p.Description;
            var filter = new ContainsDigitsFilter<Photo>();

            var result = filter.Apply(propertySelector, "").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void NotEqualsFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, Guid>> propertySelector = p => p.Guid;
            var filter = new NotEqualsFilter<Photo, Guid>();

            var result = filter.Apply(propertySelector, _photos.Last().Guid.ToString()).Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void GreaterThanFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, int>> propertySelector = p => p.Width;
            var filter = new GreaterThanFilter<Photo, int>();

            var result = filter.Apply(propertySelector, "1500").Compile().Invoke(_photos.Last());

            Assert.True(result);
        }

        [Fact]
        public void GreaterThanOrEqualFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, int>> propertySelector = p => p.Height;
            var filter = new GreaterThanOrEqualFilter<Photo, int>();

            var result = filter.Apply(propertySelector, "1500").Compile().Invoke(_photos.Last());

            Assert.True(result);
        }

        [Fact]
        public void LessThanFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, int>> propertySelector = p => p.Width;
            var filter = new LessThanFilter<Photo, int>();

            var result = filter.Apply(propertySelector, "1000").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void LessThanOrEqualFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, int>> propertySelector = p => p.Height;
            var filter = new LessThanOrEqualFilter<Photo, int>();

            var result = filter.Apply(propertySelector, "600").Compile().Invoke(_photos.First());

            Assert.True(result);
        }

        [Fact]
        public void InFilter_AppliesCorrectly()
        {
            Expression<Func<Photo, ImageTypes>> propertySelector = p => p.ImageType;
            var filter = new InFilter<Photo, ImageTypes>();

            var result = filter.Apply(propertySelector, "Jpg,Png").Compile().Invoke(_photos.First());

            Assert.True(result);
        }
    }
}