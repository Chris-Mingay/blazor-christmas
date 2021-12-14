using CleanArchitecture.Application.Dto;

namespace CleanArchitecture.Application.ApplicationUser.Dtos
{
    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
