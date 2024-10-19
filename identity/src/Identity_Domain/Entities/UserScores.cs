namespace Identity_Domain.Entities;
public class UserScores
{
    public int Id { get; set; } 
    public int Score { get; set; }
    public int UserId { get; set; }
    public int Points { get; set; }
    public string IncorrectQuestion { get; set; }
    public DateTime LastUpdated { get; set; }
}
