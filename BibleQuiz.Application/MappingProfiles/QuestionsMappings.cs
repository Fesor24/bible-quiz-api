using AutoMapper;
using BibleQuiz.Application.Features.Objective.Query;
using BibleQuiz.Application.Features.Theory.Commands.CreateQuestions;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestions;
using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Application.MappingProfiles;
public sealed class QuestionsMappings : Profile
{
    public QuestionsMappings()
    {
        CreateMap<Theory, GetQuestionResponse>();

        CreateMap<CreateQuestion, Theory>();

        CreateMap<ObjectiveEntity, ObjectiveQuestionResponse>()
            .ForMember(c => c.OptionA, d => d.MapFrom(s => s.Options.OptionA))
            .ForMember(c => c.OptionB, d => d.MapFrom(s => s.Options.OptionB))
            .ForMember(c => c.OptionC, d => d.MapFrom(s => s.Options.OptionC))
            .ForMember(c => c.OptionD, d => d.MapFrom(s => s.Options.OptionD));
    }
}
