using Microsoft.AspNetCore.Authorization;

namespace Spg.AloMalo.Api.AuthPolicies
{
    public class ForbiddenFirstNameRequirement : IAuthorizationRequirement
    {
        public string ForbiddenFirstName { get; private set; }

        public ForbiddenFirstNameRequirement(string forbiddenFirstName)
        {
            ForbiddenFirstName = forbiddenFirstName;
        }
    }
}
