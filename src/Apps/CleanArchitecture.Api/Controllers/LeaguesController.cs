using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Leagues.Commands.CreateLeague;
using CleanArchitecture.Application.Leagues.Commands.DeleteLeague;
using CleanArchitecture.Application.Leagues.Commands.UpdateLeague;
using CleanArchitecture.Application.Leagues.Dtos;
using CleanArchitecture.Application.Leagues.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Authorize]
public class LeaguesController : BaseApiController
{
    [HttpGet]
    public async Task<ServiceResult<List<LeagueDto>>> Get()
    {
        return await Mediator.Send(new GetLeaguesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ServiceResult<LeagueDto>> Get(Guid id)
    {
        return await Mediator.Send(new GetLeagueQuery() { LeagueId = id });
    }

    [HttpPost]
    public async Task<ServiceResult<Guid>> Create(CreateLeagueCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateLeagueCommand command)
    {
        if (id != command.LeagueId)
        {
            return BadRequest();
        }

        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await Mediator.Send(new DeleteLeagueCommand() { LeagueId = id });

        return NoContent();
    }
}
