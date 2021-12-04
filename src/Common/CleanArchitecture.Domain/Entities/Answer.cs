using System;

namespace CleanArchitecture.Domain.Entities;

public class Answer
{
    public Guid Id { get; set; }
    public Guid QuestionId { get; set; }
    public Guid UserProfileId { get; set; }
    public Guid? QuestionOptionId { get; set; }
    public DateTime? QuestionRevealedAt { get; set; }
    public DateTime? AnswerSubmittedAt { get; set; }
    public bool Correct { get; set; }
    public decimal AnswerTime { get; set; }
    
    public virtual Question Question { get; set; }
    public virtual UserProfile UserProfile { get; set; }
    public virtual QuestionOption SelectedAnswer { get; set; }
}