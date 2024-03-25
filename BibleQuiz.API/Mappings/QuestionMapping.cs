using AutoMapper;
using BibleQuiz.API.Dtos.Questions;
using BibleQuiz.Application.Features.Theory.Commands.CreatePastQuestions;
using BibleQuiz.Application.Features.Theory.Commands.CreateQuestions;

namespace BibleQuiz.API.Mappings;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        CreateMap<CreatePastQuestionsDto, PastQuestion>();
        CreateMap<CreateQuestionDto, CreateQuestion>();
    }
}
