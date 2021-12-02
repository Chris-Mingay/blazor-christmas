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

public class GetLeaguesQuery : IRequestWrapper<List<LeagueDto>>
{
    
}

public class GetLeaguesQueryHandler : IRequestHandlerWrapper<GetLeaguesQuery, List<LeagueDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetLeaguesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<LeagueDto>>> Handle(GetLeaguesQuery request, CancellationToken cancellationToken)
    {
        return ServiceResult.Success(await _context.Leagues
            .ProjectTo<LeagueDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken));
    }
}