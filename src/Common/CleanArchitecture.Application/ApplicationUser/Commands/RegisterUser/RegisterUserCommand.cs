using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.ApplicationUser.Queries.GetToken;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Event;

namespace CleanArchitecture.Application.ApplicationUser.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequestWrapper<LoginResponse>
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string Name { get; set; }
    }

    public class RegisterUserCommandHandler : IRequestHandlerWrapper<RegisterUserCommand, LoginResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly IApplicationDbContext _context;

        public RegisterUserCommandHandler(IIdentityService identityService, ITokenService tokenService, IApplicationDbContext context)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<ServiceResult<LoginResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var createUserReponse = await _identityService.CreateUserAsync(request.Email, request.Password);

            if (!createUserReponse.Result.Succeeded)
                return ServiceResult.Failed<LoginResponse>(ServiceError.UserFailedToCreate);
            
            var user = await _identityService.GetUserFromUserId(createUserReponse.UserId);
            var userProfile = new UserProfile()
            {
                UserId = user.Id,
                Name = request.Name
            };

            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync(cancellationToken);
            
            return ServiceResult.Success(new LoginResponse
            {
                User = user,
                Token = _tokenService.CreateJwtSecurityToken(user.Id)
            });    
            
        }
    }
}