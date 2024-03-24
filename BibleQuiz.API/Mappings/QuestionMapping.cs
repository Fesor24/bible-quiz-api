using AutoMapper;
using BibleQuiz.API.Dtos.Questions;
using BibleQuiz.Application.Features.Theory.Commands.CreatePastQuestions;

namespace BibleQuiz.API.Mappings;

public class QuestionMapping : Profile
{
    public QuestionMapping()
    {
        CreateMap<CreatePastQuestionsDto, PastQuestion>();
    }
}
