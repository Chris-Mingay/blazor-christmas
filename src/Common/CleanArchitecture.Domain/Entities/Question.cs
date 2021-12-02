using System;

namespace CleanArchitecture.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public int DayNumber { get; set; }
    public string Category { get; set; }
    public string Text { get; set; }
    public string MoreInfo { get; set; }
    public bool Published { get; set; }
    public Guid CorrectAnswerId { get; set; }
    
    public QuestionOption CorrectAnswer { get; set; }
}