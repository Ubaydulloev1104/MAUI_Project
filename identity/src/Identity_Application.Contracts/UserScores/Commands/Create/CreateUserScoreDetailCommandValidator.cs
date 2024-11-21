using FluentValidation;

namespace Identity_Application.Contracts.UserScores.Commands.Create;

public class CreateUserScoreDetailCommandValidator : AbstractValidator<CreateUserScoreDetailCommand>
{
    public CreateUserScoreDetailCommandValidator()
    {
        RuleFor(e => e.Score).NotEmpty();
        RuleFor(e => e.IncorrectQuestion).NotEmpty();
        RuleFor(e => e.LastUpdated).NotEmpty();
    }
}
