using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.Claim.Queries;
using Identity_Application.Contracts.Claim.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Claims.Queries;

public class GetBySlugQueryHandler : IRequestHandler<GetBySlugQuery, UserClaimsResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBySlugQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserClaimsResponse> Handle(GetBySlugQuery request, CancellationToken cancellationToken)
    {
        var claim = await _context.UserClaims.SingleOrDefaultAsync(s => s.Slug == request.Slug.Trim().ToLower(),
            cancellationToken);
        _ = claim ?? throw new NotFoundException("claim not found");
        return new UserClaimsResponse
        {
            Username = claim.Slug.Split('-').First(),
            ClaimType = claim.ClaimType!,
            ClaimValue = claim.ClaimValue!,
            Slug = claim.Slug
        };
    }
}
