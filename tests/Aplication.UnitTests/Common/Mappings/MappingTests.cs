using System;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Questions.Dtos;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace CleanArchitecture.Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(config => 
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }


        [Test]
        [TestCase(typeof(Question), typeof(QuestionDto))]
        [TestCase(typeof(Question), typeof(QuestionPreviewDto))]
        [TestCase(typeof(Answer), typeof(AnswerDto))]
        [TestCase(typeof(QuestionOption), typeof(QuestionOptionDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }

        [Test]
        public void ShouldMappingCorrectly()
        {
            var question = new Question() { Id = Guid.NewGuid(), Text = "ShouldMappingCorrectly", Category = "Category", DayNumber = 1};
            var questionDto = _mapper.Map<Question, QuestionDto>(question);
            questionDto.Text.Should().Be("ShouldMappingCorrectly");
        }
    }
}
