﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UnauthorizeException();
            }

            return user.UserName;
        }

        public async Task<ApplicationUserDto> CheckUserPassword(string email, string password)
        {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                ApplicationUserDto applicationUserDto = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return applicationUserDto;

            }

            return null;
        }

        public async Task<ApplicationUserDto> GetUserFromUserId(string userId)
        {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                ApplicationUserDto applicationUserDto = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return applicationUserDto;
            }

            return null;
        }
        
        public async Task<ApplicationUserDto> GetUserFromEmail(string userEmail)
        {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user != null)
            {
                ApplicationUserDto applicationUserDto = new()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return applicationUserDto;
            }

            return null;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user);

            return (result.ToApplicationResult(), user.Id);
        }
        
        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> UserIsInRole(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
    }
}
