using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.UserProfiles.Commands.UpdateName;

public class UpdateNameCommand : IRequestWrapper<bool>
{
    public Guid NameId { get; set; }
    public string Name { get; set; }
}

public class UpdateNameCommandHandler : IRequestHandlerWrapper<UpdateNameCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateNameCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ServiceResult<bool>> Handle(UpdateNameCommand request, CancellationToken cancellationToken)
    {
        var userProfile = await _context.UserProfiles.Where(x => x.UserId == _currentUserService.UserId)
            .FirstOrDefaultAsync(cancellationToken);

        userProfile.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);

        return new ServiceResult<bool>(true);

    }
}
