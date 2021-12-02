using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Leagues.Commands.JoinLeague;

public class JoinLeagueCommand : IRequestWrapper<Guid>
{
    public Guid LeagueId { get; set; }
    public string InviteCode { get; set; }
}

public class JoinLeagueCommandHandler : IRequestHandlerWrapper<JoinLeagueCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public JoinLeagueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<Guid>> Handle(JoinLeagueCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken);

        var membership = await _context.LeagueMemberships.FirstOrDefaultAsync(x =>
            x.LeagueId == request.LeagueId && x.UserProfileId == userProfile.Id);

        if (membership == null)
        {
            membership = new LeagueMembership() { LeagueId = request.LeagueId, UserProfileId = userProfile.Id };
            _context.LeagueMemberships.Add(membership);
            await _context.SaveChangesAsync(cancellationToken);    
        }
        
        return ServiceResult.Success(membership.Id);
    }
}
