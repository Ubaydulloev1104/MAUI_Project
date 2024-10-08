﻿using MediatR;

namespace Identity_Application.Contracts.ApplicationRoles.Commands;

public class UpdateRoleCommand : IRequest<Guid>
{
    public string Slug { get; set; }
    public string NewRoleName { get; set; }
}
