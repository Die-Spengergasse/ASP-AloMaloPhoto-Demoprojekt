using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spg.AloMalo.Repository.Filters;

namespace Spg.AloMalo.Repository.Test.Filters
{
    [TestClass]
    public class GenericFilterTests
    {
        private class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }

        [TestMethod]
        public void TestFilters()
        {
            List<Person> people = new List<Person>
            {
                new Person { FirstName = "John", LastName = "Doe", Age = 30, Email = "john.doe@example.com" },
                new Person { FirstName = "Jane", LastName = "Doe", Age = 25, Email = "jane.doe@example.com" },
                new Person { FirstName = "Jim", LastName = "Beam", Age = 35, Email = "jim.beam@example.com" }
            };

            var filter = new GenericFilter<Person>();

            var doeFilter = filter.EqualsFilter(nameof(Person.LastName), "Doe");
            var does = people.Where(doeFilter).ToList();
            Assert.AreEqual(2, does.Count);

            var startsWithJFilter = filter.StartsWithFilter(nameof(Person.FirstName), "J");
            var startsWithJ = people.Where(startsWithJFilter).ToList();
            Assert.AreEqual(3, startsWithJ.Count);

            var containsExampleFilter = filter.ContainsFilter(nameof(Person.Email), "example");
            var containsExample = people.Where(containsExampleFilter).ToList();
            Assert.AreEqual(3, containsExample.Count);

            var notAge30Filter = filter.NotFilter(filter.EqualsFilter(nameof(Person.Age), 30));
            var notAge30 = people.Where(notAge30Filter).ToList();
            Assert.AreEqual(2, notAge30.Count);
        }
    }
}
