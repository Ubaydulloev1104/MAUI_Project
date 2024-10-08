﻿using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.Claim.Commands;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Identity_Application.Features.Claims.Commands;

public class CreateClaimCommandHandler : IRequestHandler<CreateClaimCommand, Guid>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    public CreateClaimCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<Guid> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        _ = user ?? throw new ValidationException("user is not found");
        var claim = new ApplicationUserClaim
        {
            UserId = user.Id,
            ClaimType = request.ClaimType,
            ClaimValue = request.ClaimValue,
            Slug = user.UserName + "-" + request.ClaimType
        };
        await _context.UserClaims.AddAsync(claim, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return user.Id;

    }
}
