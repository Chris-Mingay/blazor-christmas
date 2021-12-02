using System;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Leagues.Dtos;

public class LeagueDto : IMapFrom<League>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int NumberOfMembers { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<League, LeagueDto>()
            .ForMember(d => d.NumberOfMembers, opt => opt.MapFrom(s => s.LeagueMemberships.Count));
    }
    
}