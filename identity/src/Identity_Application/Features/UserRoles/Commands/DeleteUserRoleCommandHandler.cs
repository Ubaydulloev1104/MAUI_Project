﻿using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.UserRoles.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserRoles.Commands;

public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserRoleCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userRole = await _context.UserRoles.FirstOrDefaultAsync(ur => ur.Slug == request.Slug);
        _ = userRole ?? throw new NotFoundException("not found");
        _context.UserRoles.Remove(userRole);
        await _context.SaveChangesAsync();
        return true;
    }
}
