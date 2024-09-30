using AutoMapper;
using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.Profile.Commands.UpdateProfile;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserProfiles.Commands;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserHttpContextAccessor _userHttpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateProfileCommandHandler(UserManager<ApplicationUser> userManager,
        IUserHttpContextAccessor userHttpContextAccessor,
        IMapper mapper)
    {
        _userManager = userManager;
        _userHttpContextAccessor = userHttpContextAccessor;
        _mapper = mapper;
    }
    public async Task<bool> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(_userHttpContextAccessor.GetUserName());
        _ = user ?? throw new NotFoundException("user is not found");

        var exitingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName != user.UserName &&
        (u.PhoneNumber == request.PhoneNumber || u.Email == request.Email));
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

        if (user.Email != request.Email)
            user.EmailConfirmed = false;
        if (user.PhoneNumber != request.PhoneNumber) user.PhoneNumberConfirmed = false;
        _mapper.Map(request, user);


        return (await _userManager.UpdateAsync(user)).Succeeded;

    }
}
