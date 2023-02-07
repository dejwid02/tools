using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Movies.API;

public class AuthorizationService : IAuthorizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext.User.Claims
            .First(c => c.Properties.Select(p => p.Value).Contains("oid")).Value == "70f9b210-979d-47ba-8192-a684d64a6f74";
    }
}