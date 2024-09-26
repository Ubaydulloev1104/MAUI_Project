using MediatR;

namespace Identity_Application.Contracts.UserRoles.Commands
{
	public class DeleteUserRoleCommand : IRequest<bool>
	{
		public string Slug { get; set; }
	}
}
