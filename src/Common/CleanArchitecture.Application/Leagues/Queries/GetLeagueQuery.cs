using System;
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

public class GetLeagueQuery : IRequestWrapper<LeagueDto>
{
    public Guid LeagueId { get; set; }
}

public class GetLeagueQueryHandler : IRequestHandlerWrapper<GetLeagueQuery, LeagueDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLeagueQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<LeagueDto>> Handle(GetLeagueQuery request, CancellationToken cancellationToken)
    {
        var league = await _context.Leagues
            .Where(x => x.Id == request.LeagueId)
            .ProjectTo<LeagueDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        league.LeagueMemberships =
            league.LeagueMemberships.OrderByDescending(x => x.Points).ThenBy(x => x.TotalAnswerTime).ToList();

        return league is not null ? ServiceResult.Success(league) : ServiceResult.Failed<LeagueDto>(ServiceError.NotFound);

    }
}