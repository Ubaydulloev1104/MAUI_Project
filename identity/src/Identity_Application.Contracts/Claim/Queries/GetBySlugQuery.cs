using Identity_Application.Contracts.Claim.Responses;
using MediatR;

namespace Identity_Application.Contracts.Claim.Queries;

public class GetBySlugQuery : IRequest<UserClaimsResponse>
{
    public string Slug { get; set; } = "";
}
