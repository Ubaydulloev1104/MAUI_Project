using MediatR;
using Identity_Application.Contracts.User.Responses;

namespace Identity_Application.Contracts.User.Queries;

#nullable disable
public record GetAccessTokenUsingRefreshTokenQuery : IRequest<JwtTokenResponse>
{
	public string RefreshToken { get; set; }
	public string AccessToken { get; set; }
}
