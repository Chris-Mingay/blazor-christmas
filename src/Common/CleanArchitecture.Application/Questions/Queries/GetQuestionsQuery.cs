using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Questions.Dtos;
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

    public GetQuestionsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<QuestionPreviewDto>>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions =  await _context.Questions
            .ProjectTo<QuestionPreviewDto>(_mapper.ConfigurationProvider)
            .OrderBy(x => x.DayNumber)
            .ToListAsync(cancellationToken);
        
        return questions.Count > 0 ? ServiceResult.Success(questions) : ServiceResult.Failed<List<QuestionPreviewDto>>(ServiceError.NotFound);
    }
}