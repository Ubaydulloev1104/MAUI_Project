using Identity_Application.Contracts.User.Responses;
using MediatR;


namespace Identity_Application.Contracts.User.Queries;

public class GetUserByUsernameQuery : IRequest<UserResponse>
{
	public string UserName { get; set; }
}
