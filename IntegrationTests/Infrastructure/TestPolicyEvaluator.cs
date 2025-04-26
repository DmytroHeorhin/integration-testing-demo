using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace IntegrationTests.Infrastructure
{
    public class TestPolicyEvaluator(IAuthorizationService authorization, string email) : PolicyEvaluator(authorization)
    {
        public override async Task<AuthenticateResult> AuthenticateAsync(AuthorizationPolicy policy, HttpContext context)
        {
            var principal = new ClaimsPrincipal();

            var claims = new[]
            {
                new Claim("email", email)
            };

            principal.AddIdentity(new ClaimsIdentity(claims, "TestScheme"));

            return await Task.FromResult(AuthenticateResult.Success(
                new AuthenticationTicket(
                    principal,
                    new AuthenticationProperties(),
                    "TestScheme")));
        }
    }
}
