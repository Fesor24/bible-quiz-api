using BibleQuiz.Application.Features.TestQuestion.Commands.CreateQuestion;
using BibleQuiz.Application.Features.TestQuestion.Commands.DeleteQuestion;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestions;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestionsBySource;
using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BibleQuiz.API.Extensions;
using BibleQuiz.Application.Features.TestQuestion.Commands.CreatePastQuestions;
using BibleQuiz.Application.Features.TestQuestion.Commands.UpdateQuestionPassage;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestionPassage;

namespace BibleQuiz.API.Controllers;

[ApiController]
public class QuestionsController : ControllerBase
{
    private readonly ISender _sender;

    public QuestionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/api/questions")]
    [ProducesResponseType(typeof(IReadOnlyList<GetQuestionResponse>),StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> GetQuestionsBySource(QuestionSource source)
    {
        var res = await _sender.Send(new GetQuestionsBySourceRequest { Source = source });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpPost("/api/questions")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddQuestions(List<CreateQuestionDto> questions)
    {
        var res = await _sender.Send(new CreateQuestionsCommand { Questions = questions });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpDelete("/api/question/{id}")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var res = await _sender.Send(new DeleteQuestionCommand { Id = id });

        return res.Match(value => NoContent(), this.HandleErrorResult);
    }

    [HttpPost("/api/question")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddQuestion(CreateQuestionDto question)
    {
        var res = await _sender.Send(new CreateQuestionCommand
        {
            Answer = question.Answer,
            Question = question.Question,
            Verse = question.Verse
        });

        return res.Match(value => CreatedAtRoute(value, question), this.HandleErrorResult);
    }

    [HttpPost("/api/past-questions")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddPastQuestions([FromBody] List<CreatePastQuestionsDto> questions, 
        [FromQuery] QuestionSource source)
    {
        var res = await _sender.Send(new CreatePastQuestionsCommand
        {
            Questions = questions,
            Source = source
        });

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

    [HttpPost("/api/question/passage")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> UpdateQuestionPassage(UpdateQuestionPassageDto passage)
    {
        var res = await _sender.Send(new UpdateQuestionPassageCommand { Data = passage });

        return res.Match(value => NoContent(), this.HandleErrorResult);
    }

    [HttpGet("/api/question/passage/{questionId}")]
    [ProducesResponseType(typeof(GetQuestionPassageResponse), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> GetQuestionPassage(int questionId)
    {
        var res = await _sender.Send(new GetQuestionPassageRequest(questionId));

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }
}
