using BibleQuiz.API.Dtos.Questions;
using BibleQuiz.API.Extensions;
using BibleQuiz.Application.Features.Objective.Command.CreateObjective;
using BibleQuiz.Application.Features.Objective.Query;
using BibleQuiz.Application.Features.Theory.Commands.CreatePastQuestions;
using BibleQuiz.Application.Features.Theory.Commands.CreateQuestion;
using BibleQuiz.Application.Features.Theory.Commands.CreateQuestions;
using BibleQuiz.Application.Features.Theory.Commands.DeleteQuestion;
using BibleQuiz.Application.Features.Theory.Commands.UpdateQuestionPassage;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestionPassage;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestions;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestionsBySource;
using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers;
public class QuestionsController : BaseController<QuestionsController>
{
    [HttpGet("/api/questions/theory")]
    [ProducesResponseType(typeof(IReadOnlyList<GetQuestionResponse>),StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> GetQuestionsBySource(QuestionSource source)
    {
        if (!Enum.IsDefined(typeof(QuestionSource), source))
            return BadRequest(new Error("Invalid.Source", $"{source} is not part of valid sources"));

        var res = await Sender.Send(new GetQuestionsBySourceRequest { Source = source });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpPost("/api/questions/theory")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddQuestions(List<CreateQuestionDto> questions)
    {
        var res = await Sender.Send(new CreateQuestionsCommand 
        { 
            Questions = Mapper.Map<List<CreateQuestion>>(questions) 
        });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpDelete("/api/question/theory/{id}")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var res = await Sender.Send(new DeleteQuestionCommand { Id = id });

        return res.Match(value => NoContent(), this.HandleErrorResult);
    }

    [HttpPost("/api/question/theory")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddQuestion(CreateQuestionDto question)
    {
        var res = await Sender.Send(new CreateQuestionCommand
        {
            Answer = question.Answer,
            Question = question.Question,
            Book = question.Book,
            VerseTo = question.VerseTo,
            VerseFrom = question.VerseFrom,
            Chapter = question.Chapter
        });

        return res.Match(value => CreatedAtRoute(value.Id, value), this.HandleErrorResult);
    }

    [HttpPost("/api/past-questions/theory")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddPastQuestions([FromBody] List<CreatePastQuestionsDto> questions, 
        [FromQuery] QuestionSource source)
    {
        var res = await Sender.Send(new CreatePastQuestionsCommand
        {
            Questions = Mapper.Map<List<PastQuestion>>(questions),
            Source = source
        });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpPost("/api/question/theory/passage")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> UpdateQuestionPassage(UpdateQuestionPassageDto question)
    {
        var res = await Sender.Send(new UpdateQuestionPassageCommand(question.QuestionId, question.Passage));

        return res.Match(value => NoContent(), this.HandleErrorResult);
    }

    [HttpGet("/api/question/theory/passage/{questionId}")]
    [ProducesResponseType(typeof(GetQuestionPassageResponse), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> GetQuestionPassage(int questionId)
    {
        var res = await Sender.Send(new GetQuestionPassageRequest(questionId));

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpPost("api/question/objective")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<IActionResult> AddObjectiveQuestion([FromBody] CreateObjectiveQuestionDto model)
    {
        var res = await Sender.Send(new CreateObjectiveCommand(model.Question, model.OptionA,
            model.OptionB, model.OptionC, model.OptionD, model.Answer));

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpGet("api/question/objectives")]
    [ProducesResponseType(typeof(ObjectiveQuestionResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllObjectiveQuestions()
    {
        var res = await Sender.Send(new GetAllObjectiveQuestionsQuery());

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }
}
