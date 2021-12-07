using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Queries;

public class GetQuestionsQuery : IRequestWrapper<List<QuestionPreviewDto>>
{
    
}

public class GetQuestionsQueryHandler : IRequestHandlerWrapper<GetQuestionsQuery, List<QuestionPreviewDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetQuestionsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<List<QuestionPreviewDto>>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken); 
        
        var questions =  await _context.Questions
            .ProjectTo<QuestionPreviewDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.DayNumber)
            .ToListAsync(cancellationToken);

        var answers = userProfile is not null
            ? await _context.Answers.Where(x => x.UserProfileId == userProfile.Id)
                .ToListAsync(cancellationToken)
            : new List<Answer>();

        foreach (var question in questions)
        {
            var answer = answers.FirstOrDefault(x => x.QuestionId == question.Id);
            if (answer is not null)
            {
                question.Correct = answer.Correct;
                question.Incorrect = answer.AnswerSubmittedAt.HasValue && !answer.Correct;
            }
        }
        
        return questions.Count > 0 ? ServiceResult.Success(questions) : ServiceResult.Failed<List<QuestionPreviewDto>>(ServiceError.NotFound);
    }
}