namespace Spg.General.IdentityProvider.Models
{
    public record UserInformationDto(
        string UserName,
        string FirstName,
        string LastName,
        string EMail,
        string Role)
    {
        public string Token { get; set; }
        public string? Signature { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        public static UserInformationDto GetFakeData()
        {
            return new UserInformationDto("martin", "Martin", "Schrutek", "schrutek@spengegasse.at", "admin");
        }
    }
}
