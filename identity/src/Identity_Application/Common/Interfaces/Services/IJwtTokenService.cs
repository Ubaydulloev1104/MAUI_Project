using System.Security.Claims;

namespace Identity_Application.Common.Interfaces.Services;

public interface IJwtTokenService
{
    public string CreateTokenByClaims(IList<Claim> user, out DateTime expireDate);
    public string CreateRefreshToken(IList<Claim> user);
}
