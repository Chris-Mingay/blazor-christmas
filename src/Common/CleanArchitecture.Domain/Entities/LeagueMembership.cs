using System;

namespace CleanArchitecture.Domain.Entities;

public class LeagueMembership
{
    public Guid Id { get; set; }
    public Guid LeagueId { get; set; }
    public Guid UserProfileId { get; set; }

    public virtual League League { get; set; }
    public virtual UserProfile UserProfile { get; set; }
}