using System;
using System.Linq;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Leagues.Dtos;

public class LeagueMembershipDto : IMapFrom<LeagueMembership>
{
    public Guid Id { get; set; }
    public Guid LeagueId { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public int Rank { get; set; }
    public decimal TotalAnswerTime { get; set; }
    public string LeagueName { get; set; }
    public int NumberOfMembers { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<LeagueMembership, LeagueMembershipDto>()
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.UserProfile.Name))
            .ForMember(d => d.LeagueName, opt => opt.MapFrom(s => s.League.Name))
            .ForMember(d => d.NumberOfMembers, opt => opt.MapFrom(s => s.League.LeagueMemberships.Count()))
            .ForMember(d => d.Points, opt => opt.MapFrom(s => s.UserProfile.Answers.Count(x => x.Correct)))
            .ForMember(d => d.TotalAnswerTime, opt => opt.MapFrom(s => s.UserProfile.Answers.Sum(x => x.AnswerTime)));
    }
    
}