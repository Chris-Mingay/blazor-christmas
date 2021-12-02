using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Questions.Commands.SubmitAnswer;

public class SubmitAnswerCommand : IRequestWrapper<Guid>
{
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
    public Guid QuestionOptionId { get; set; }
}

public class SubmitAnswerCommandHandler : IRequestHandlerWrapper<SubmitAnswerCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public SubmitAnswerCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<Guid>> Handle(SubmitAnswerCommand request, CancellationToken cancellationToken)
    {

        var question = await _context.Questions.FindAsync(request.QuestionId);
        
        var answer = await _context.Answers.FindAsync(request.AnswerId);
        answer.QuestionOptionId = request.QuestionOptionId;
        answer.Correct = answer.QuestionOptionId == question.CorrectAnswerId;
        answer.AnswerSubmittedAt = DateTime.Now;

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(answer.Id);
    }
}
