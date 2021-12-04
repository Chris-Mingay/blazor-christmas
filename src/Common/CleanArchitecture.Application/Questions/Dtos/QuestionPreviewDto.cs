using System;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Questions.Dtos;

public class QuestionPreviewDto : IMapFrom<Question>
{
    public Guid Id { get; set; }
    public int DayNumber { get; set; }
    public string Category { get; set; }
    public bool Published { get; set; }
    public bool Correct { get; set; }
    public bool Incorrect { get; set; }
}