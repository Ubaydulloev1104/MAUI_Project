using Identity_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Common.Interfaces.DbContexts;

public interface IApplicationDbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationUserClaim> UserClaims { get; set; }
    public DbSet<ApplicationUserRole> UserRoles { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<IdentityRoleClaim<Guid>> RoleClaims { get; set; }
    public DbSet<UserScores> UserScores { get; set; }
    
}
