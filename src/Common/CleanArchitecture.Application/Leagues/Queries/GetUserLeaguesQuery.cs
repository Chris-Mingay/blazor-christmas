using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Leagues.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Leagues.Queries;

public class GetUserLeaguesQuery : IRequestWrapper<List<LeagueMembershipDto>>
{

}

public class GetUserLeaguesQueryHandler : IRequestHandlerWrapper<GetUserLeaguesQuery, List<LeagueMembershipDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetUserLeaguesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<List<LeagueMembershipDto>>> Handle(GetUserLeaguesQuery request, CancellationToken cancellationToken)
    {
        
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken);

        var leagueMemberships = await _context.LeagueMemberships
            .Where(x => x.UserProfileId == userProfile.Id)
            .ProjectTo<LeagueMembershipDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.LeagueName)
            .ToListAsync(cancellationToken);

        foreach (var membership in leagueMemberships)
        {
            int rank = _context.LeagueMemberships.Where(x => x.LeagueId == membership.LeagueId)
                .OrderByDescending(x => x.UserProfile.Answers.Count(x => x.Correct))
                .ThenBy(x => x.UserProfile.Answers.Sum(y => y.AnswerTime))
                .AsEnumerable()
                .Select((entry, index) => new { UserProfileId = entry.UserProfileId, Rank = index + 1 })
                .FirstOrDefault(x=>x.UserProfileId == userProfile.Id).Rank;
            membership.Rank = rank;
        }
        
        return ServiceResult.Success(leagueMemberships);
        
        
        
    }
}