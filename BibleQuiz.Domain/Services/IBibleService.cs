using BibleQuiz.Domain.Models;
using BibleQuiz.Domain.Shared;

namespace BibleQuiz.Domain.Services;
public interface IBibleService
{
    Task<Result<BibleVerseSearchResponse, Error>> GetScripturesAsync(BibleVerse model);

    Task<Result<BibleBooks, Error>> GetBibleBooks();
}
