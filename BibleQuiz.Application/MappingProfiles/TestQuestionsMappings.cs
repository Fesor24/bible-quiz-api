using AutoMapper;
using BibleQuiz.Application.Features.Theory.Commands.CreateQuestions;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestions;
using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Application.MappingProfiles;
public sealed class TestQuestionsMappings : Profile
{
    public TestQuestionsMappings()
    {
        CreateMap<Theory, GetQuestionResponse>();

        CreateMap<CreateQuestionDto, Theory>();
    }
}
