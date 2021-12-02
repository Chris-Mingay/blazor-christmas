using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Leagues.Commands.LeaveLeague;

public class LeaveLeagueCommandValidator : AbstractValidator<LeaveLeagueCommand>
{
    public LeaveLeagueCommandValidator(IApplicationDbContext _context)
    {
        RuleFor(v => v.LeagueId).Must(id =>
            {
                var league = _context.Leagues.Find(id);
                return league is not null;
            })
            .WithMessage("League must exist");
        
    }
}
