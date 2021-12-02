using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Questions.Commands.Create;

public class CreateQuestionCommand : IRequestWrapper<Guid>
{
    public string Category { get; set; }
    public string Text { get; set; }
    public int DayNumber { get; set; }
}

public class CreateQuestionCommandHandler : IRequestHandlerWrapper<CreateQuestionCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateQuestionCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {

        var question = new Question()
        {
            Text = request.Text,
            Category = request.Category,
            DayNumber = request.DayNumber
        };
        await _context.Questions.AddAsync(question, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(question.Id);
    }
}