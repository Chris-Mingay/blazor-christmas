using CleanArchitecture.Domain.Common;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Event
{
    public class QuestionActivatedEvent : DomainEvent
    {
        public QuestionActivatedEvent(Question question)
        {
            Question = question;
        }

        public Question Question { get; }
    }
}
