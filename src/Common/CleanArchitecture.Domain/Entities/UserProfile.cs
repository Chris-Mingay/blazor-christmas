using System;
using System.Collections.Generic;

namespace CleanArchitecture.Domain.Entities;

public class UserProfile
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string Name { get; set; }
    public List<Answer> Answers { get; set; }
}