using FluentValidation;

namespace CleanArchitecture.Application.ApplicationUser.Queries.GetExternalToken
{
    public class GetExternalTokenQueryValidator : AbstractValidator<GetExternalTokenQuery>
    {
        public GetExternalTokenQueryValidator()
        {
            RuleFor(v => v.Provider)
                .NotEmpty()
                .WithMessage("External provider name required");

            RuleFor(v => v.IdToken)
                .NotEmpty().WithMessage("Id Token required");
        }
    }
}
