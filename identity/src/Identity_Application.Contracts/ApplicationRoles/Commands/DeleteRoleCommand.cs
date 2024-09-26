using MediatR;

namespace Identity_Application.Contracts.ApplicationRoles.Commands;

public class DeleteRoleCommand : IRequest<bool>
{
    public string Slug { get; set; }
}
