using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Commands.SubmitAnswer;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Application.Questions.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers;

[Authorize]
public class QuestionsController : BaseApiController
{
    [HttpGet]
    public async Task<ServiceResult<List<QuestionPreviewDto>>> Get()
    {
        return await Mediator.Send(new GetQuestionsQuery());
    }

    [HttpGet("preview/{id}")]
    public async Task<ServiceResult<QuestionPreviewDto>> GetPreview(Guid id)
    {
        return await Mediator.Send(new GetQuestionPreviewQuery() { QuestionId = id });
    }
    
    [HttpGet("{id}")]
    public async Task<ServiceResult<QuestionVM>> Get(Guid id)
    {
        return await Mediator.Send(new GetQuestionQuery() { QuestionId = id });
    }

    [HttpPost("{questionId}")]
    public async Task<ActionResult> SubmitAnswer(Guid questionId, SubmitAnswerCommand command)
    {
        if (questionId != command.QuestionId)
        {
            return BadRequest();
        }
        await Mediator.Send(command);
        return NoContent();
    }

}
