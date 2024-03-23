using BibleQuiz.API.Extensions;
using BibleQuiz.Application.Features.BibleBooks.Command.CreateBibleBooks;
using BibleQuiz.Application.Features.BibleBooks.Query.GetBibleBooks;
using BibleQuiz.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers;

public class BibleBooksController : BaseController<BibleBooksController>
{
    private readonly IBibleService _bibleService;

    public BibleBooksController(IBibleService bibleService)
    {
        _bibleService = bibleService;
    }

    [HttpGet("/api/kjv-bible-books")]
    public async Task<IActionResult> GetBibleBooks()
    {
        var res = await Sender.Send(new GetBibleBooksRequest());

        return res.Match(val => Ok(val), this.HandleErrorResult);
    }

    [HttpGet("/api/kjv/scriptures")]
    public async Task<IActionResult> GetScriptures([FromQuery] string verse)
    {
        return Ok(await _bibleService.GetScripturesAsync(new Domain.Models.BibleVerse { Verse = verse }));
    }

    [HttpGet("api/kjv/load/bible-books")]
    public async Task<IActionResult> LaodBibleBooks()
    {
        var res = await Sender.Send(new CreateBibleBooksCommand());

        return res.Match(value => Ok(value), this.HandleErrorResult);
    }

}
