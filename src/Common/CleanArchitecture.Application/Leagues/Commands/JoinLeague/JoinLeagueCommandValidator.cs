using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Leagues.Commands.JoinLeague;

public class JoinLeagueCommandValidator : AbstractValidator<JoinLeagueCommand>
{
    public JoinLeagueCommandValidator(IApplicationDbContext _context)
    {
        RuleFor(v => v.LeagueId).Must(id =>
            {
                var league = _context.Leagues.Find(id);
                return league is not null;
            })
            .WithMessage("League must exist");
        
        RuleFor(v => v).Must(command =>
            {
                var league = _context.Leagues.Find(command.LeagueId);
                return league != null && league.InviteCode == command.InviteCode;
            })
            .WithMessage("Incorrect invite code");
        
    }
}
