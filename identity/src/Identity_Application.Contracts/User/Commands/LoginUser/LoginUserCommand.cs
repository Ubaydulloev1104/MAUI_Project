using Identity_Application.Contracts.User.Responses;
using MediatR;

namespace Identity_Application.Contracts.User.Commands.LoginUser
{
	public class LoginUserCommand : IRequest<JwtTokenResponse>
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
}
