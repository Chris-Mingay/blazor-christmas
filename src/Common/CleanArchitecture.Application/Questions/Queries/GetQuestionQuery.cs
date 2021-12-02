using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Questions.Queries;

public class GetQuestionQuery : IRequestWrapper<QuestionVM>
{
    public Guid QuestionId { get; set; }
}

public class GetQuestionQueryHandler : IRequestHandlerWrapper<GetQuestionQuery, QuestionVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetQuestionQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<QuestionVM>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
    {

        QuestionVM response = new QuestionVM();
        
        var question = await _context.Questions
            .Where(x=>x.Id == request.QuestionId)
            .ProjectTo<QuestionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        response.Question = question;
        
        var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x=>x.UserId == _currentUserService.UserId, cancellationToken);
        
        // Get an existing answer object for this user if it exists
        var answer =
            await _context.Answers
                .Where(x => x.QuestionId == request.QuestionId && x.UserProfileId == userProfile.Id)
                .FirstOrDefaultAsync(cancellationToken);

        // If it doesn't exist we create one and set the revealed time.
        if (answer == null)
        {
            answer = new Answer()
            {
                UserProfileId = userProfile.Id, QuestionId = request.QuestionId, QuestionRevealedAt = DateTime.Now
            };
            _context.Answers.Add(answer);
            
            await _context.SaveChangesAsync(cancellationToken);
        }

        response.Answer = _mapper.Map<Answer, AnswerDto>(answer);
            
        return ServiceResult.Success(response);
    }
}