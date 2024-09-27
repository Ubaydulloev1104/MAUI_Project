using Identity_Application.Common.Exceptions;
using Identity_Application.Contracts.ApplicationRoles.Commands;
using Identity_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.Role.Commands;

public class UpdateRolCommandHandler : IRequestHandler<UpdateRoleCommand, Guid>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    public UpdateRolCommandHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Guid> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Slug == request.Slug);
        _ = role ?? throw new NotFoundException("Role not found");
        role.Name = request.NewRoleName;
        var result = await _roleManager.UpdateAsync(role);
        return result.Succeeded
                 ? role.Id
                 : throw new ValidationException("Failed to update role.");
    }
}
