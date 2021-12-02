using System;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Questions.Dtos;

public class AnswerDto : IMapFrom<Answer>
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Guid? QuestionOptionId { get; set; }
    public DateTime? QuestionRevealedAt { get; set; }
    public DateTime? AnswerSubmittedAt { get; set; }
    public bool Correct { get; set; }
    
}