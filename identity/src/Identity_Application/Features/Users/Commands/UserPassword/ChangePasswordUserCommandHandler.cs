using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.User.Commands.ChangePassword;
using Identity_Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity_Application.Features.Users.Commands.UserPassword;

public class ChangePasswordUserCommandHandler : IRequestHandler<ChangePasswordUserCommand, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserHttpContextAccessor _userHttpContextAccessor;

    public ChangePasswordUserCommandHandler(UserManager<ApplicationUser> userManager, IUserHttpContextAccessor userHttpContextAccessor)
    {
        _userManager = userManager;
        _userHttpContextAccessor = userHttpContextAccessor;
    }

    public async Task<bool> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(_userHttpContextAccessor.GetUserName());
        if (user == null)
            throw new NotFoundException("User not found");

        bool success = await _userManager.CheckPasswordAsync(user, request.OldPassword);
        if (!success)
            throw new Exception("Incorrect old password");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!result.Succeeded)
            return false;

        return true;
    }
}
