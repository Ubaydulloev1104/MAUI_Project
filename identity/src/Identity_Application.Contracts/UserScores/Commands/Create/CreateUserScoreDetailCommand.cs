using MediatR;

namespace Identity_Application.Contracts.UserScores.Commands.Create;

public class CreateUserScoreDetailCommand : IRequest<Guid>
{
    public int Score { get; set; }
    public string IncorrectQuestion { get; set; }
    public DateTime LastUpdated { get; set; }
}
