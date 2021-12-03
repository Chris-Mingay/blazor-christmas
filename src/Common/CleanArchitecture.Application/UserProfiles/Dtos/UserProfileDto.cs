using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.UserProfiles.Dtos;

public class UserProfileDto : IMapFrom<UserProfile>
{
    public string Name { get; set; }
}