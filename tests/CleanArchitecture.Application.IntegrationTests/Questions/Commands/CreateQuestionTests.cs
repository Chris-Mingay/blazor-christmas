using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Questions.Commands.Create;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Questions.Commands
{
    public class CreateQuestionTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateQuestionCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        /*[Test]
        public async Task ShouldRequireUniqueName()
        {
            await SendAsync(new CreateQuestionCommand
            {
                Text = "ShouldRequireUniqueName",
                Category = "Category",
                DayNumber = 1
            });

            var command = new CreateQuestionCommand
            {
                Text = "ShouldRequireUniqueName",
                Category = "Category",
                DayNumber = 1
            };

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }*/

        [Test]
        public async Task ShouldCreateQuestion()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateQuestionCommand
            {
                Text = "ShouldCreateQuestion",
                Category = "Category",
                DayNumber = 1
            };

            var result = await SendAsync(command);

            var list = await FindAsync<Question>(result.Data);

            list.Should().NotBeNull();
            list.Text.Should().Be(command.Text);
            list.Category.Should().Be("Category");
            list.DayNumber.Should().Be(1);
        }
    }
}
