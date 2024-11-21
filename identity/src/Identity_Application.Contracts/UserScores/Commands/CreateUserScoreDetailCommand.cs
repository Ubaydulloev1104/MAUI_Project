using MediatR;

namespace Identity_Application.Contracts.UserScores.Commands;

public class CreateUserScoreDetailCommand : IRequest<Guid>
{
    public string Slug { get; set; }
    public int Score { get; set; }
    public string IncorrectQuestion { get; set; }
    public DateTime LastUpdated { get; set; }
}
