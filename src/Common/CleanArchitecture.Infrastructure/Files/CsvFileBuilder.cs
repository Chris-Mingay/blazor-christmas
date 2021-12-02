using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Infrastructure.Files.Maps;
using CsvHelper;

namespace CleanArchitecture.Infrastructure.Files
{
    public class CsvFileBuilder : ICsvFileBuilder
    {
        public byte[] BuildDistrictsFile(IEnumerable<QuestionDto> questions)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                csvWriter.Context.RegisterClassMap<QuestionMap>();
                csvWriter.WriteRecords(questions);
            }

            return memoryStream.ToArray();
        }
    }
}
