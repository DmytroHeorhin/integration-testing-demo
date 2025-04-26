using Microsoft.AspNetCore.Http;

namespace Domain.UserContext;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetUserEmail()
    {
        var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value ??
            throw new InvalidOperationException("User email is not available in the current context.");
            
        return userEmail;
    }
}