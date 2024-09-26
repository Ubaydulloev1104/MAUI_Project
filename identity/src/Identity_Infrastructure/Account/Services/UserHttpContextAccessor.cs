using Identity_Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;


namespace Identity_Infrastructure.Account.Services;

public class UserHttpContextAccessor : IUserHttpContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var user = _httpContextAccessor.HttpContext?.User;

        var idClaim = user?.FindFirst(Identity_Application.ClaimTypes.Id);

        if (idClaim != null && Guid.TryParse(idClaim.Value, out Guid id))
            return id;

        return Guid.Empty;
    }

    public string GetUserName()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var userNameClaim = user?.FindFirst(Identity_Application.ClaimTypes.Username);

        return userNameClaim != null ? userNameClaim.Value : string.Empty;
    }

    public List<string> GetUserRoles()
    {
        var user = _httpContextAccessor.HttpContext?.User;
        var roleClaims = user?.FindAll(Identity_Application.ClaimTypes.Role);

        return roleClaims?.Select(rc => rc.Value).ToList() ?? new List<string>();
    }
}
