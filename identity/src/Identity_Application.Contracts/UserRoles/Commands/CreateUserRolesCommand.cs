using MediatR;

namespace Identity_Application.Contracts.UserRoles.Commands
{
	public class CreateUserRolesCommand : IRequest<string>
	{
		public string RoleName { get; set; }
		public string UserName { get; set; }

	}
}
