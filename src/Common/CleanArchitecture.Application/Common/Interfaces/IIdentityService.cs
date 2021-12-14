using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUserDto> CheckUserPassword(string userName, string password);

        Task<ApplicationUserDto> GetUserFromUserId(string userId);
        Task<ApplicationUserDto> GetUserFromEmail(string userEmail);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName);

        Task<bool> UserIsInRole(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);
    }
}
