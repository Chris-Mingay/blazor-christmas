using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.UserProfiles.Commands.UpdateName;
using CleanArchitecture.Application.UserProfiles.Dtos;
using CleanArchitecture.Application.UserProfiles.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Authorize]
public class UserProfilesController : BaseApiController
{
    private readonly ICurrentUserService _currentUserService;

    public UserProfilesController(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ServiceResult<UserProfileDto>> Get()
    {
        var userId = _currentUserService.UserId;
        return await Mediator.Send(new GetUserProfileQuery()
        {
            UserId = userId
        });
    }
    
    [HttpPut]
    public async Task<ServiceResult<bool>> UpdateName(UpdateNameCommand command)
    {
        return await Mediator.Send(command);
    }
}