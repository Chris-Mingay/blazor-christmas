using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace CleanArchitecture.Application.Questions.Queries;

public class GetQuestionPreviewQuery : IRequestWrapper<QuestionPreviewDto>
{
    public Guid QuestionId { get; set; }
}

public class GetQuestionPreviewQueryHandler : IRequestHandlerWrapper<GetQuestionPreviewQuery, QuestionPreviewDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetQuestionPreviewQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<QuestionPreviewDto>> Handle(GetQuestionPreviewQuery request, CancellationToken cancellationToken)
    {
        var question = await _context.Questions
            .Where(x=>x.Id == request.QuestionId)
            .ProjectTo<QuestionPreviewDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return question is not null ? ServiceResult.Success<QuestionPreviewDto>(question) : ServiceResult.Failed<QuestionPreviewDto>(ServiceError.NotFound);
    }
}