using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Infrastructure.Identity;
using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            var defaultUser = new ApplicationUser { UserName = "test@test.com", Email = "test@test.com" };
            var anotherUser = new ApplicationUser { UserName = "another@test.com", Email = "another@test.com" };

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Password1!");
            }
            if (userManager.Users.All(u => u.UserName != anotherUser.UserName))
            {
                await userManager.CreateAsync(anotherUser, "Password1!");
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            
            var users = userManager.Users.ToList();
            
            if (!context.UserProfiles.Any())
            {
                
                foreach (var user in users)
                {
                    await context.UserProfiles.AddAsync(new UserProfile()
                    {
                        UserId = user.Id,
                        Name = user.Email
                    });
                    await context.SaveChangesAsync();
                }
            }

            if (!context.Leagues.Any())
            {

                var userProfile = context.UserProfiles.FirstOrDefault();
                var anotherUserProfile = context.UserProfiles.LastOrDefault();

                var league = new League()
                {
                    Name = "Test League",
                    UserProfileId = userProfile?.Id ?? Guid.Empty,
                };

                context.Leagues.Add(league);
                await context.SaveChangesAsync();

                var leagueMembership = new LeagueMembership()
                {
                    LeagueId = league.Id,
                    UserProfileId = userProfile?.Id ?? Guid.Empty,
                };
                context.LeagueMemberships.Add(leagueMembership);

                if (anotherUserProfile is not null)
                {
                    context.LeagueMemberships.Add(new LeagueMembership()
                    {
                        LeagueId = league.Id,
                        UserProfileId = anotherUserProfile.Id
                    });
                }
                
                await context.SaveChangesAsync();
                
            }

            if (!context.Questions.Any())
            {
                int day = DateTime.Now.Day;

                var questions = new List<Question>();
                for (int i = 0; i < 25; i++)
                {
                    questions.Add(new Question()
                    {
                        DayNumber = (i + 1),
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                        Published = (i < day)
                    });
                }

                foreach (var question in questions)
                {
                    question.QuestionOptions = new List<QuestionOption>();
                    for (int i = 0; i < 4; i++)
                    {
                        var o = new QuestionOption()
                        {
                            Text = $"Option {(i + 1)}"
                        };
                        question.QuestionOptions.Add(o);
                    }
                }

                await context.Questions.AddRangeAsync(questions);
                await context.SaveChangesAsync();

                foreach (var question in questions)
                {
                    question.CorrectAnswerId = question.QuestionOptions[0].Id;
                }

                await context.SaveChangesAsync();
            }
        }
    }
}