﻿using Identity_Application.Contracts.User.Responses;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Identity_Application.Contracts.User.Queries.CheckUserDetails;

namespace Identity_Application.Features.Users.Queries;

public class CheckUserDetailsQueryHandler : IRequestHandler<CheckUserDetailsQuery, UserDetailsResponse>
{
    public CheckUserDetailsQueryHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    private readonly UserManager<ApplicationUser> _userManager;

    public async Task<UserDetailsResponse> Handle(CheckUserDetailsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        var userWithPhone = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
        var userWithEmail = await _userManager.FindByEmailAsync(request.Email);

        return new UserDetailsResponse
        {
            IsUserNameTaken = user != null,
            IsPhoneNumberTaken = userWithPhone != null,
            IsEmailTaken = userWithEmail != null
        };
    }
}
