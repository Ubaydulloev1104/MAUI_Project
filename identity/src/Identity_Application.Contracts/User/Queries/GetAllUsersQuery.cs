using Identity_Application.Contracts.User.Responses;
using MediatR;

namespace Identity_Application.Contracts.User.Queries
{
	public class GetAllUsersQuery : IRequest<List<UserResponse>>
	{
	}
}
