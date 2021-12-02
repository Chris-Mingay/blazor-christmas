using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Leagues.Commands.CreateLeague;

public class CreateLeagueCommandValidator : AbstractValidator<CreateLeagueCommand>
{
    public CreateLeagueCommandValidator(IApplicationDbContext _context)
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("League name required and cannot be more than 100 characters");
        
    }
}
