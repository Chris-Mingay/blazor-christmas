using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Leagues.Dtos;

public class LeagueDto : IMapFrom<League>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<LeagueMembershipDto> LeagueMemberships { get; set; }
    public int NumberOfMembers => LeagueMemberships?.Count ?? 0;

}