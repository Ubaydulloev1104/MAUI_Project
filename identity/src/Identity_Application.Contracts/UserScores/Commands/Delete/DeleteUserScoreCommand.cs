using MediatR;

namespace Identity_Application.Contracts.UserScores.Commands.Delete;

public class DeleteUserScoreCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
