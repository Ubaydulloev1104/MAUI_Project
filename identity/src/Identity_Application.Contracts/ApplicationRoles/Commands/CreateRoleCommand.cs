using MediatR;

namespace Identity_Application.Contracts.ApplicationRoles.Commands;

public class CreateRoleCommand : IRequest<Guid>
{
    public string RoleName { get; set; }
}
