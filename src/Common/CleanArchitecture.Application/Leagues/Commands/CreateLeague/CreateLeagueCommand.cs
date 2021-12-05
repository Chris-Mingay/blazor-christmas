using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Leagues.Commands.CreateLeague;

public class CreateLeagueCommand : IRequestWrapper<Guid>
{
    public string Name { get; set; }
}

public class CreateLeagueCommandHandler : IRequestHandlerWrapper<CreateLeagueCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateLeagueCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<Guid>> Handle(CreateLeagueCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken);

        var league = new League() { Name = request.Name, UserProfileId = userProfile.Id };
        _context.Leagues.Add(league);
        await _context.SaveChangesAsync(cancellationToken);
        
        _context.LeagueMemberships.Add(new LeagueMembership()
        {
            LeagueId = league.Id,
            UserProfileId = userProfile.Id
        });
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(league.Id);
    }
}
