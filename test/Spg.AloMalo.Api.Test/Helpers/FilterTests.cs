using System;

public class FilterTests
{
	public FilterTests()
	{
        var persons = new List<Person>
{
    new Person { FirstName = "John", LastName = "Doe", Age = 30, Email = "john.doe@example.com" },
    new Person { FirstName = "Jane", LastName = "Smith", Age = 25, Email = "jane.smith@example.com" }
};

        var filterService = new FilterService<Person>();

        filterService.AddFilter(new EqualsFilter<Person, string>(p => p.LastName, "Doe"));
        filterService.AddFilter(new ContainsFilter<Person>(p => p.Email, "example"));

        var filteredPersons = filterService.ApplyFilters(persons);

        foreach (var person in filteredPersons)
        {
            Console.WriteLine($"{person.FirstName} {person.LastName}");
        }

    }
}
