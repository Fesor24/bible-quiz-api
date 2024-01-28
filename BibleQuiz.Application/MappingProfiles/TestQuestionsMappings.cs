using AutoMapper;
using BibleQuiz.Application.Features.TestQuestion.Commands.CreateQuestion;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestions;
using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Application.MappingProfiles;
public sealed class TestQuestionsMappings : Profile
{
    public TestQuestionsMappings()
    {
        CreateMap<TestQuestion, GetQuestionResponse>();

        CreateMap<CreateQuestionsDto, TestQuestion>();
    }
}
