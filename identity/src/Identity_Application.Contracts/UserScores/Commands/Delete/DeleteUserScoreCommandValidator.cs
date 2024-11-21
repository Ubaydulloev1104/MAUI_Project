using FluentValidation;

namespace Identity_Application.Contracts.UserScores.Commands.Delete;

public class DeleteUserScoreCommandValidator : AbstractValidator<DeleteUserScoreCommand>
{
    public DeleteUserScoreCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
    }
}
