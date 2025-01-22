using MauiApp1.DB.Entities;

namespace MauiApp1.DB;

public class ServerData
{
    public Guid UserId { get; set; }
    public int TotalScore { get; set; }
    public List<IncorrectAnswer> IncorrectAnswers { get; set; }
}
