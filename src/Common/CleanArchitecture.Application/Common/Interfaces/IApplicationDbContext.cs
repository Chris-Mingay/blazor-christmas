using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Answer> Answers { get; }
        DbSet<League> Leagues { get; }
        DbSet<LeagueMembership> LeagueMemberships { get; }
        DbSet<Question> Questions { get; }
        DbSet<QuestionOption> QuestionOptions { get; }
        DbSet<UserProfile> UserProfiles { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
