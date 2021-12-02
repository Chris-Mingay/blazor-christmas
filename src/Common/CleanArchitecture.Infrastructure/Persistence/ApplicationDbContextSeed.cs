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

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Password1!");
                //await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            if (!context.UserProfiles.Any())
            {
                var users = userManager.Users.ToList();
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

            if (!context.Questions.Any())
            {
                var questions = new List<Question>
                {
                    new Question()
                    {
                        DayNumber = 1,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 2,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 3,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 4,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 5,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 6,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 7,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 8,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 9,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 10,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 11,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 12,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 13,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 14,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 15,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 16,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 17,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 18,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 19,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 20,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 21,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 22,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 23,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 24,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    },
                    new Question()
                    {
                        DayNumber = 25,
                        Category = "Sport",
                        Text = "Question Text?",
                        MoreInfo = "More Info",
                    }
                };

                await context.Questions.AddRangeAsync(questions);
                await context.SaveChangesAsync();
            }
        }
    }
}