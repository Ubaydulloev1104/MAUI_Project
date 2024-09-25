using Microsoft.AspNetCore.Identity;

namespace Identity_Domain.Entities;

public class ApplicationUserRole : IdentityUserRole<Guid>
{
    public string Slug { get; set; }
}
