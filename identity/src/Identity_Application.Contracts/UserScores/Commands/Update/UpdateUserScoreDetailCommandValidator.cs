using FluentValidation;

namespace Identity_Application.Contracts.UserScores.Commands.Update;

public class UpdateUserScoreDetailCommandValidator : AbstractValidator<UpdateUserScoreDetailCommand>
{
    public UpdateUserScoreDetailCommandValidator()
    {
        RuleFor(e => e.Id).NotEmpty();
        RuleFor(e => e.LastUpdated).NotEmpty();
        RuleFor(e => e.IncorrectQuestion).NotEmpty();
        RuleFor(e => e.Score).NotEmpty();
    }
}
