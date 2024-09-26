using MediatR;
using Identity_Application.Contracts.Profile.Responses;

namespace Identity_Application.Contracts.Profile.Queries;

public class GetPofileQuery : IRequest<UserProfileResponse>
{
	public string UserName { get; set; }
}
