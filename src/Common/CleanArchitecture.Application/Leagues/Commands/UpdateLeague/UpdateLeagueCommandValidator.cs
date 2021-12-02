using System.Linq;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Leagues.Commands.UpdateLeague;

public class UpdateLeagueCommandValidator : AbstractValidator<UpdateLeagueCommand>
{
    public UpdateLeagueCommandValidator(IApplicationDbContext _context, ICurrentUserService _currentUserService)
    {
        RuleFor(v => v.LeagueId)
            .Must(id =>
            {
                var league = _context.Leagues.Find(id);
                return league is not null;
            })
            .WithMessage("League not found");
        
        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("League name required and cannot be more than 100 characters");
        
        RuleFor(v => v.LeagueId)
            .Must(id =>
            {
                var league = _context.Leagues.Find(id);
                if (league is null) return false;
                
                var userProfile = _context.UserProfiles
                    .FirstOrDefault(x => x.UserId == _currentUserService.UserId);
                if (userProfile is null) return false;

                return league.UserProfileId == userProfile.Id;
            })
            .WithMessage("Only founders can update leagues");

    }
}
