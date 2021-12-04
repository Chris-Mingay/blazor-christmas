using System.Linq;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Leagues.Dtos;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.UserProfiles.Dtos;

public class UserProfileDto : IMapFrom<UserProfile>
{
    public string Name { get; set; }
    public int Points { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserProfile, UserProfileDto>()
            .ForMember(d => d.Points, opt => opt.MapFrom(s => s.Answers.Count(x=>x.Correct)));

    }
    
}