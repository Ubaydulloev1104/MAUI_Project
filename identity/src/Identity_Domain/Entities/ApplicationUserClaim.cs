using Microsoft.AspNetCore.Identity;

namespace Identity_Domain.Entities;

public class ApplicationUserClaim : IdentityUserClaim<Guid>
{
    public string Slug { get; set; }
}
