using Spg.AloMalo.Application.Services.Filter;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.AloMalo.Application.Test
{
    public class FilterTests
    {
        public class TestEntity
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string[] Tags { get; set; }
        }

        public class EqualsFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression()
            {
                // Arrange
                var filter = new EqualsFilter<TestEntity>();
                var value = "John";

                // Act
                var expression = filter.GetFilterExpression("Name", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Name = "John" });

                // Assert
                Assert.True(result);
            }
        }

        public class StartsWithFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression()
            {
                // Arrange
                var filter = new StartsWithFilter<TestEntity>();
                var value = "Jo";

                // Act
                var expression = filter.GetFilterExpression("Name", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Name = "John" });

                // Assert
                Assert.True(result);
            }
        }

        public class EndsWithFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression()
            {
                // Arrange
                var filter = new EndsWithFilter<TestEntity>();
                var value = "hn";

                // Act
                var expression = filter.GetFilterExpression("Name", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Name = "John" });

                // Assert
                Assert.True(result);
            }
        }

        public class GreaterThanFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression()
            {
                // Arrange
                var filter = new GreaterThanFilter<TestEntity>();
                var value = "30";

                // Act
                var expression = filter.GetFilterExpression("Age", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Age = 35 });

                // Assert
                Assert.True(result);
            }
        }

        public class ContainsFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression_ForString()
            {
                // Arrange
                var filter = new ContainsFilter<TestEntity>();
                var value = "oh";

                // Act
                var expression = filter.GetFilterExpression("Name", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Name = "John" });

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression_ForIEnumerable()
            {
                // Arrange
                var filter = new ContainsFilter<TestEntity>();
                var value = "tag1";

                // Act
                var expression = filter.GetFilterExpression("Tags", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Tags = new[] { "tag1", "tag2" } });

                // Assert
                Assert.True(result);
            }
        }

        public class RegexFilterTests
        {
            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression()
            {
                // Arrange
                var filter = new RegexFilter<TestEntity>();
                var value = "^J.*n$";

                // Act
                var expression = filter.GetFilterExpression("Name", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Name = "John" });

                // Assert
                Assert.True(result);
            }

            [Fact]
            public void GetFilterExpression_ReturnsCorrectExpression_ForIEnumerable()
            {
                // Arrange
                var filter = new RegexFilter<TestEntity>();
                var value = "^t.*1$";

                // Act
                var expression = filter.GetFilterExpression("Tags", value);
                var compiledExpression = expression.Compile();
                var result = compiledExpression(new TestEntity { Tags = new[] { "tag1", "tag2" } });

                // Assert
                Assert.True(result);
            }
        }

        public class FilterFactoryTests
        {
            [Theory]
            [InlineData("equals", typeof(EqualsFilter<TestEntity>))]
            [InlineData("startswith", typeof(StartsWithFilter<TestEntity>))]
            [InlineData("endswith", typeof(EndsWithFilter<TestEntity>))]
            [InlineData("greaterthan", typeof(GreaterThanFilter<TestEntity>))]
            [InlineData("contains", typeof(ContainsFilter<TestEntity>))]
            public void GetFilter_ReturnsCorrectFilterType(string operation, Type expectedType)
            {
                // Act
                var filter = FilterFactory<TestEntity>.GetFilter(operation);

                // Assert
                Assert.IsType(expectedType, filter);
            }

            [Fact]
            public void GetFilter_ThrowsNotSupportedException_ForUnknownOperation()
            {
                // Arrange
                var operation = "unknown";

                // Act & Assert
                Assert.Throws<NotSupportedException>(() => FilterFactory<TestEntity>.GetFilter(operation));
            }
        }
    }
}
