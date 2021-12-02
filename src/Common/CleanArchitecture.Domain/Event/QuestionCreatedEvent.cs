using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Event
{
    public class QuestionCreatedEvent : DomainEvent
    {
        public QuestionCreatedEvent(Question question)
        {
            Question = question;
        }

        public Question Question { get; }
    }
}
