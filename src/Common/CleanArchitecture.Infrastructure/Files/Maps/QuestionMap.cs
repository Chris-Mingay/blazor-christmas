using System.Globalization;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Questions.Dtos;
using CsvHelper.Configuration;

namespace CleanArchitecture.Infrastructure.Files.Maps
{
    public sealed class QuestionMap : ClassMap<QuestionDto>
    {
        public QuestionMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.QuestionOptions).Convert(_ => "");
        }
    }
}
