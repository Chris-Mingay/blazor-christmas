using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Leagues.Commands.UpdateLeague;

public class UpdateLeagueCommand : IRequestWrapper<bool>
{
    public Guid LeagueId { get; set; }
    public string Name { get; set; }
}

public class UpdateLeagueCommandHandler : IRequestHandlerWrapper<UpdateLeagueCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateLeagueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateLeagueCommand request, CancellationToken cancellationToken)
    {
        var league = await _context.Leagues.FindAsync(request.LeagueId);
        league.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);
        return ServiceResult.Success(true);
    }
}
