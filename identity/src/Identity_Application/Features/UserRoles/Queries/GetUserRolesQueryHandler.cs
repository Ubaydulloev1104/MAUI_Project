﻿using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.UserRoles.Queries;
using Identity_Application.Contracts.UserRoles.Response;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserRoles.Queries
{
    public class GetUserRolesQueryHandler : IRequestHandler<GetUserRolesQuery, List<UserRolesResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetUserRolesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserRolesResponse>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
        {
            return await NewMethodForGetUserRole(request, cancellationToken);
        }

        private async Task<List<UserRolesResponse>> NewMethodForGetUserRole(GetUserRolesQuery request,
            CancellationToken cancellationToken)
        {
            IQueryable<ApplicationUserRole> query = _context.UserRoles;
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                var user = await _context.Users.FirstOrDefaultAsync(s =>
                        s.NormalizedUserName != null && s.NormalizedUserName.Contains(request.UserName!.ToUpper()),
                    cancellationToken: cancellationToken);
                if (user != null)
                {
                    query = query.Where(s => s.UserId == user.Id);
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Role))
            {
                var role = await _context.Roles.FirstOrDefaultAsync(s =>
                        s.NormalizedName != null && s.NormalizedName.Contains(request.Role!.ToUpper()),
                    cancellationToken: cancellationToken);
                if (role != null)
                {
                    query = query.Where(s => s.RoleId == role.Id);
                }
            }

            var raw = await query.ToArrayAsync(cancellationToken: cancellationToken);
            var res = new List<UserRolesResponse>();
            foreach (var userRole in raw)
            {
                res.Add(new UserRolesResponse
                {
                    UserName = (await _context.Users.FirstAsync(s => s.Id == userRole.UserId, cancellationToken: cancellationToken)).UserName!,
                    RoleName = (await _context.Roles.FirstAsync(s => s.Id == userRole.RoleId, cancellationToken: cancellationToken)).Name!,
                    Slug = userRole.Slug
                });
            }

            return res;
        }
    }
}
