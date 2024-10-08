﻿using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.User.Commands.RegisterUser;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _context;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext context,
     RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<Guid> Handle(RegisterUserCommand request,
        CancellationToken cancellationToken)
    {

        var exitingUser = await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber || u.Email == request.Email);
        if (exitingUser != null)
        {
            if (exitingUser.Email == request.Email && exitingUser.PhoneNumber == request.PhoneNumber)
            {
                throw new DuplicateWaitObjectException($"Email {request.Email} and Phone Number {request.PhoneNumber} are not available!");
            }
            else if (exitingUser.PhoneNumber == request.PhoneNumber)
            {
                throw new DuplicateWaitObjectException($"Phone Number {request.PhoneNumber} is not available!");
            }
            else if (exitingUser.Email == request.Email)
            {
                throw new DuplicateWaitObjectException($"Email {request.Email} is not available!");
            }
        }

        ApplicationUser user = new()
        {
            Id = Guid.NewGuid(),
            UserName = request.Username,
            NormalizedUserName = request.Username.ToLower(),
            Email = request.Email,
            NormalizedEmail = request.Email.ToLower(),
            EmailConfirmed = false,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = new DateTime(2000, 1, 1),
        };
        IdentityResult result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException(result.Errors.First().Description);
        }

        var role = await _context.Roles.FirstOrDefaultAsync(s => s.NormalizedName != null && s.NormalizedName.Contains(request.Role.ToUpper()), cancellationToken: cancellationToken);
        if (role == null)
        {
            role = new ApplicationRole
            {
                Id = Guid.NewGuid(),
                Name = request.Role,
                NormalizedName = _roleManager.NormalizeKey(request.Role),
                Slug = $"{request.Username}-{request.Role}",
            };
            var roleResult = await _roleManager.CreateAsync(role);
            if (!roleResult.Succeeded)
            {
                throw new ValidationException(roleResult.Errors.First().Description);

            }
        }

        var userRole = new ApplicationUserRole { UserId = user.Id, RoleId = role.Id, Slug = $"{user.UserName}-role" };
        await _context.UserRoles.AddAsync(userRole, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        await CreateClaimAsync(request.Role, user.UserName, user.Id, user.Email, user.PhoneNumber, request.Application,
        cancellationToken);
        return user.Id;
    }


    private async Task CreateClaimAsync(string role, string username, Guid id, string email, string phone,
string application, CancellationToken cancellationToken = default)
    {
        var userClaims = new[]
        {
        new ApplicationUserClaim
        {
            UserId = id,
            ClaimType = ClaimTypes.Role,
            ClaimValue = role,
            Slug = $"{username}-role"
        },
        new ApplicationUserClaim
        {
            UserId = id,
            ClaimType = ClaimTypes.Id,
            ClaimValue = id.ToString(),
            Slug = $"{username}-id"
        },
        new ApplicationUserClaim
        {
            UserId = id,
            ClaimType = ClaimTypes.Username,
            ClaimValue = username,
            Slug = $"{username}-username"
        },
        new ApplicationUserClaim
        {
            UserId = id,
            ClaimType = ClaimTypes.Email,
            ClaimValue = email,
            Slug = $"{username}-email"
        },
        new ApplicationUserClaim
        {
            UserId = id,
            ClaimType = ClaimTypes.Application,
            ClaimValue = application,
            Slug = $"{username}-application"
        }
    };

        await _context.UserClaims.AddRangeAsync(userClaims, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
