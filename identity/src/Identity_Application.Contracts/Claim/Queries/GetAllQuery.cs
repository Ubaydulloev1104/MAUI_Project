using Identity_Application.Contracts.Claim.Responses;
using MediatR;

namespace Identity_Application.Contracts.Claim.Queries;

public class GetAllQuery : IRequest<List<UserClaimsResponse>>
{
    public string Username { get; set; } = null;
    public string ClaimType { get; set; } = null;
}
