namespace Spg.AloMalo.DomainModel.Model
{
    public record Address(string StreetNumber, string ZipCode, string City, string Country)
    {
        public State State { get; set; } = default!;
        // TODO: Logik ... lassen wir uns noch einfallen
    }

    public class State
    {
        public string Name { get; set; } = string.Empty;
    }
}
