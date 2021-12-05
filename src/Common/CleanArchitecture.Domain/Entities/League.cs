using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class League
{
    public Guid Id { get; set; }
    public Guid UserProfileId { get; set; }
    public string Name { get; set; }
    public List<LeagueMembership> LeagueMemberships { get; set; }
    public UserProfile UserProfile { get; set; }
}