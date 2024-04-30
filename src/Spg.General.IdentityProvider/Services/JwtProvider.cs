using Microsoft.IdentityModel.Tokens;
using Spg.General.IdentityProvider.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Spg.General.IdentityProvider.Services
{
    // https://www.infoworld.com/article/3669188/how-to-implement-jwt-authentication-in-aspnet-core.html
    // https://www.oauth.com/oauth2-servers/access-tokens/access-token-response/
    public class JwtProvider
    {
        private readonly IConfiguration _configuration;

        public JwtProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserInformationDto GenerateToken(LoginDto credentials, TimeSpan lifetime)
        {
            // Validation (against DB)
            if (credentials.UserName != "martin" 
                || HashHelper.CalculateHash(credentials.Password, _configuration["Jwt:Key"] ?? "")
                != HashHelper.CalculateHash("123", _configuration["Jwt:Key"] ?? ""))
            {
                throw AuthenticationException.FromWrongCredentials();
            }

            // DB Stuff...
            UserInformationDto userInformation = UserInformationDto.GetFakeData();

            // Act
            var issuer = credentials.Issuer ?? _configuration["Jwt:Issuer"];
            var audience = credentials.Audience ?? _configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"] ?? "");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, userInformation.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, userInformation.EMail),
                    new Claim(JwtRegisteredClaimNames.FamilyName, userInformation.LastName),
                    new Claim(JwtRegisteredClaimNames.GivenName, userInformation.FirstName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // Rolle des Benutzer als ClaimTypes.DefaultRoleClaimType
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, credentials.Role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            userInformation.Token = stringToken;
            return userInformation;
        }
    }
}
