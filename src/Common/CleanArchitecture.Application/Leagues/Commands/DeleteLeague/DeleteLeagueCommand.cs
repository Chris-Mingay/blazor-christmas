using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Leagues.Commands.DeleteLeague;

public class DeleteLeagueCommand : IRequestWrapper<bool>
{
    public Guid LeagueId { get; set; }
}

public class DeleteLeagueCommandHandler : IRequestHandlerWrapper<DeleteLeagueCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public DeleteLeagueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<bool>> Handle(DeleteLeagueCommand request, CancellationToken cancellationToken)
    {
        
        var league = await _context.Leagues.FindAsync(request.LeagueId);
        
        var memberships = await _context.LeagueMemberships.Where(x => x.LeagueId == request.LeagueId).ToListAsync(cancellationToken);
        _context.LeagueMemberships.RemoveRange(memberships);
        _context.Leagues.Remove(league);
        await _context.SaveChangesAsync(cancellationToken);
        return ServiceResult.Success(true);
        
    }
}
