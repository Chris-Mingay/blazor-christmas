using System;
using System.Collections.Generic;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Questions.Dtos;

public class QuestionDto : IMapFrom<Question>
{
    public Guid Id { get; set; }
    public int DayNumber { get; set; }
    public string Category { get; set; }
    public string Text { get; set; }
    public string MoreInfo { get; set; }
    public bool Published { get; set; }
    public List<QuestionOptionDto> QuestionOptions { get; set; }
}