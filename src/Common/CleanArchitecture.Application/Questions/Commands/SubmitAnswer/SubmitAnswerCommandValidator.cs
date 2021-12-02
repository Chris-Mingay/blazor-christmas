using System.Linq;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.Questions.Commands.SubmitAnswer;

public class SubmitAnswerCommandValidator : AbstractValidator<SubmitAnswerCommand>
{
    public SubmitAnswerCommandValidator(IApplicationDbContext _context, ICurrentUserService _currentUserService)
    {
        RuleFor(v => v.QuestionId).Must(id =>
            {
                var question = _context.Questions.Find(id);
                return question is { Published: true };
            })
            .WithMessage("Question must be published");

        RuleFor(v => v.AnswerId).Must(id =>
            {
                var answer = _context.Answers.Find(id);
                return answer is { AnswerSubmittedAt: null };
            })
            .WithMessage("Question cannot already be answered");

        RuleFor(v => v).Must(command =>
            {
                return _context.QuestionOptions.Any(x =>
                    x.QuestionId == command.QuestionId && x.Id == command.QuestionOptionId);
            })
            .WithMessage("Option doesn't exist or isn't associated with this question");
    }
}