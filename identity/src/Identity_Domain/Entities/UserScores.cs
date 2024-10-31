namespace Identity_Domain.Entities;
public class UserScores
{
    public string Slug { get; set; }
    public int Id { get; set; }
    public int Score { get; set; }

    public string IncorrectQuestion { get; set; }
    public DateTime LastUpdated { get; set; }
    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; }
}
