using System.Linq;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Questions.Commands.Create;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator(IApplicationDbContext _context, ICurrentUserService _currentUserService)
    {

        RuleFor(v => v.Text)
            .NotEmpty()
            .WithMessage("Question text is required");
        
        RuleFor(v => v.Category)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Question category is required");

        RuleFor(v => v.DayNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Day number must be at least 1");


    }
}
