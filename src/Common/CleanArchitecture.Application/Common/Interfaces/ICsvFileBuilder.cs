using System.Collections.Generic;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Questions.Dtos;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildDistrictsFile(IEnumerable<QuestionDto> questions);
    }
}
