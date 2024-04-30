using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Spg.AloMalo.Api.AuthPolicies
{
    public class ForbiddenFirstNameHandler : AuthorizationHandler<ForbiddenFirstNameRequirement>
    {
        private readonly IConfiguration _configuration;
        public ForbiddenFirstNameHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            ForbiddenFirstNameRequirement requirement)
        {
            var user = context.User;

            Claim? firstNameClaim = user.FindFirst(
                c => c.Issuer == _configuration["Jwt:Issuer"]
                && c.Type == ClaimTypes.GivenName);

            if ((firstNameClaim?.Value.ToLower() ?? 
                requirement.ForbiddenFirstName?.ToLower()) != requirement.ForbiddenFirstName?.ToLower())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
