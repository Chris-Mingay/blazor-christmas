using System.Threading.Tasks;
using CleanArchitecture.Application.Questions.Queries;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Questions.Queries
{
    public class GetAllQuestionsTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllQuestions()
        {
            await AddAsync(new Question()
            {
                Text = "ShouldReturnAllQuestions",
                Category = "Category",
                DayNumber = 1
            });

            var query = new GetQuestionsQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().BeGreaterThan(0);
        }
    }
}