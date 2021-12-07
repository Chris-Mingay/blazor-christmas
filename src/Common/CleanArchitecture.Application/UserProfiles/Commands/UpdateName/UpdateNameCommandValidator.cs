using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Interfaces;
using FluentValidation;

namespace CleanArchitecture.Application.UserProfiles.Commands.UpdateName;

public class UpdateNameCommandValidator : AbstractValidator<UpdateNameCommand>
{
    public UpdateNameCommandValidator(IApplicationDbContext _context, ICurrentUserService _currentUserService)
    {
        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("Name required");
        

    }
}
