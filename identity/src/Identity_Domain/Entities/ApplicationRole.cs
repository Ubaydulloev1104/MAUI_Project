using Microsoft.AspNetCore.Identity;

namespace Identity_Domain.Entities;

public class ApplicationRole : IdentityRole<Guid>
{
    public string Slug { get; set; }
}
