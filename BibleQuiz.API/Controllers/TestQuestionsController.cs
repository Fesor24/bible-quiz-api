using BibleQuiz.Application.Features.TestQuestion.Commands.CreateQuestion;
using BibleQuiz.Application.Features.TestQuestion.Commands.DeleteQuestion;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestions;
using BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestionsBySource;
using BibleQuiz.Domain.Enums;
using BibleQuiz.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers;

[ApiController]
public class TestQuestionsController : ControllerBase
{
    private readonly ISender _sender;

    public TestQuestionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("/api/questions")]
    [ProducesResponseType(typeof(IReadOnlyList<GetQuestionResponse>),StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> GetQuestionsBySource(QuestionSource source)
    {
        var res = await _sender.Send(new GetQuestionsBySourceRequest { Source = source });

        return res.Match<IActionResult>(value => Ok(value), err => BadRequest(res.Error));
    }

    [HttpPost("/api/questions")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> AddQuestions(List<CreateQuestionsDto> questions)
    {
        var res = await _sender.Send(new CreateQuestionsCommand { Questions = questions });

        return res.Match<IActionResult>(value => Ok(value), err => BadRequest(err));
    }

    [HttpDelete("/api/question/:id")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesErrorResponseType(typeof(Error))]
    public async Task<IActionResult> DeleteQuestion(int id)
    {
        var res = await _sender.Send(new DeleteQuestionCommand { Id = id });

        return res.Match<IActionResult>(value => NoContent(), err => NotFound(err));
    }
}
