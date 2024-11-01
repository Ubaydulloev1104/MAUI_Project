using Microsoft.AspNetCore.Identity;
namespace Identity_Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>
{
    public DateTime DateOfBirth { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string AboutMyself { get; set; }
    public List<UserScores> UserScores { get; set; }

}
