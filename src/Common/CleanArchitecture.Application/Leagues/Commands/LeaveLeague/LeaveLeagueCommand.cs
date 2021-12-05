using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Leagues.Commands.LeaveLeague;

public class LeaveLeagueCommand : IRequestWrapper<bool>
{
    public Guid LeagueId { get; set; }
}

public class LeaveLeagueCommandHandler : IRequestHandlerWrapper<LeaveLeagueCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public LeaveLeagueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<bool>> Handle(LeaveLeagueCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken);
        
        var membership = await _context.LeagueMemberships.Where(x=>x.LeagueId == request.LeagueId && x.UserProfileId == userProfile.Id).FirstOrDefaultAsync(cancellationToken);
        if (membership is not null)
        {
            _context.LeagueMemberships.Remove(membership);
            await _context.SaveChangesAsync(cancellationToken);    
        }
        return ServiceResult.Success(true);
    }
}
