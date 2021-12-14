using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.ApplicationUser.Commands.RegisterUser;
using CleanArchitecture.Application.ApplicationUser.Dtos;
using CleanArchitecture.Application.ApplicationUser.Queries.GetExternalToken;
using CleanArchitecture.Application.ApplicationUser.Queries.GetToken;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    /// <summary>
    /// Login
    /// </summary>
    public class LoginController : BaseApiController
    {
        /// <summary>
        /// Login and get JWT token email: test@test.com password: Matech_1850
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetTokenQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
        
        /// <summary>
        /// Create a new user account and return a newly generated JWT token
        /// </summary>
        /// <param name="query">Registration details</param>
        /// <param name="cancellationToken"></param>
        /// <returns>LoginResponse</returns>
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(RegisterUserCommand query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
        
        [HttpPost("external")]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Login(GetExternalTokenQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
        
    }
}
