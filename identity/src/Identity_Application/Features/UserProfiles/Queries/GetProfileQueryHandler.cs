﻿using AutoMapper;
using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.Profile.Queries;
using Identity_Application.Contracts.Profile.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserProfiles.Queries;

public class GetProfileQueryHandler : IRequestHandler<GetPofileQuery, UserProfileResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserHttpContextAccessor _userHttpContextAccessor;

    public GetProfileQueryHandler(IApplicationDbContext context,
        IMapper mapper, IUserHttpContextAccessor userHttpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _userHttpContextAccessor = userHttpContextAccessor;
    }

    public async Task<UserProfileResponse> Handle(GetPofileQuery request, CancellationToken cancellationToken)
    {
        var userRoles = _userHttpContextAccessor.GetUserRoles();
        var userName = _userHttpContextAccessor.GetUserName();

        if (request.UserName != null && userRoles.Any(role => role == "Applicant") && userName != request.UserName)
            throw new ForbiddenAccessException("Access is denied");

        if (request.UserName != null)
            userName = request.UserName;

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
        _ = user ?? throw new NotFoundException("user is not found");
        var response = _mapper.Map<UserProfileResponse>(user);
        return response;
    }
}
