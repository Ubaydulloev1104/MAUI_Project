﻿using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.Claim.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Claims.Commands
{
    public class UpdateClaimCommandHandler : IRequestHandler<UpdateClaimCommand, Unit>
    {
        private readonly IApplicationDbContext _context;

        public UpdateClaimCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateClaimCommand request, CancellationToken cancellationToken)
        {
            var claim = await _context.UserClaims.SingleOrDefaultAsync(s => s.Slug == request.Slug.Trim().ToLower(), cancellationToken: cancellationToken);
            _ = claim ?? throw new NotFoundException($"claim with slug {request.Slug} not found");

            claim.ClaimValue = request.ClaimValue;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }
}
