using System.ComponentModel;

namespace Spg.General.IdentityProvider.Models
{
    public record LoginDto(
        string UserName,
        string Password, 
        string Role,
        string? Issuer, 
        string? Audience);
}
