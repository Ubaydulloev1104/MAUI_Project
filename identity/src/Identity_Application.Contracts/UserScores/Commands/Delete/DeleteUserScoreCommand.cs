using MediatR;

namespace Identity_Application.Contracts.UserScores.Commands.Delete;

public class DeleteUserScoreCommand : IRequest<bool>
{
    public int Id { get; set; }
}
