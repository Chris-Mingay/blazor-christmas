using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ApplicationUser.Dtos;
using CleanArchitecture.Application.ApplicationUser.Queries.GetToken;
using CleanArchitecture.Application.Common.Configuration;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authentication;

namespace CleanArchitecture.Application.ApplicationUser.Queries.GetExternalToken
{
    public class GetExternalTokenQuery : IRequestWrapper<LoginResponse>
    {
        public string Provider { get; set; }

        public string IdToken { get; set; }
    }

    public class GetExternalTokenQueryHandler : IRequestHandlerWrapper<GetExternalTokenQuery, LoginResponse>
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;
        private readonly AuthenticationGoogleOptions _authenticationGoogleOptions;
        private readonly IApplicationDbContext _context;

        public GetExternalTokenQueryHandler(IIdentityService identityService, ITokenService tokenService, AuthenticationGoogleOptions authenticationGoogleOptions, IApplicationDbContext context)
        {
            _identityService = identityService;
            _tokenService = tokenService;
            _authenticationGoogleOptions = authenticationGoogleOptions;
            _context = context;
        }

        public async Task<ServiceResult<LoginResponse>> Handle(GetExternalTokenQuery request,
            CancellationToken cancellationToken)
        {
            switch (request.Provider)
            {
                case "Google":
                    return await HandleGoogle(request, cancellationToken);
                default:
                    return ServiceResult.Failed<LoginResponse>(ServiceError.ServiceProviderNotFound);
            }
        }

        private async Task<ServiceResult<LoginResponse>> HandleGoogle(GetExternalTokenQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                
                var validationSettings = new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new string[] { _authenticationGoogleOptions.ClientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, validationSettings);
                
                if (payload == null)
                    return ServiceResult.Failed<LoginResponse>(ServiceError.ForbiddenError);

                var user = await _identityService.GetUserFromEmail(payload.Email);

                if (user == null)
                {
                    var createUserReponse = await _identityService.CreateUserAsync(payload.Email);

                    if (!createUserReponse.Result.Succeeded)
                        return ServiceResult.Failed<LoginResponse>(ServiceError.UserFailedToCreate);
            
                    user = await _identityService.GetUserFromUserId(createUserReponse.UserId);
                    var userProfile = new UserProfile()
                    {
                        UserId = user.Id,
                        Name = payload.Name
                    };
                    _context.UserProfiles.Add(userProfile);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                    
                
                return ServiceResult.Success(new LoginResponse
                {
                    User = user,
                    Token = _tokenService.CreateJwtSecurityToken(user.Id)
                });
            }
            catch (InvalidJwtException ex)
            {
                return ServiceResult.Failed<LoginResponse>(ServiceError.ServiceProvider);
            }
        }
        
    }
}