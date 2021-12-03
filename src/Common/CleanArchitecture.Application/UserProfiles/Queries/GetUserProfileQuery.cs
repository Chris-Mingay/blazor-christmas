using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.UserProfiles.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.UserProfiles.Queries;

public class GetUserProfileQuery : IRequestWrapper<UserProfileDto>
{
    public string UserId { get; set; }
}

public class GetUserProfileQueryHandler : IRequestHandlerWrapper<GetUserProfileQuery, UserProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserProfileQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles
            .Where(x=>x.UserId == request.UserId)
            .ProjectTo<UserProfileDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return userProfile is not null ? ServiceResult.Success(userProfile) : ServiceResult.Failed<UserProfileDto>(ServiceError.NotFound);

    }
}