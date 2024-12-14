using MediatR;

namespace Identity_Application.Contracts.UserScores.Commands.Update;

public class UpdateUserScoreDetailCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public string IncorrectQuestion { get; set; }
    public DateTime LastUpdated { get; set; }
}
