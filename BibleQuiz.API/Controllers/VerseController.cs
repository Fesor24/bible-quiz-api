namespace BibleQuiz.API.Controllers
{
    //[ApiController]
    //public class VerseController : ControllerBase
    //{
    //    /// <summary>
    //    /// The scoped instance of IUnitOfWork
    //    /// </summary>
    //    private readonly IUnitOfWork unit;

    //    /// <summary>
    //    /// Scoped instance of IBibleService
    //    /// </summary>
    //    private readonly IBibleService _bibleService;

    //    public VerseController(IUnitOfWork unit, IBibleService bibleService)
    //    {
    //        this.unit = unit;
    //        _bibleService = bibleService;
    //    }

    //    /// <summary>
    //    /// Endpoint to get random verse
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet(ApiRoutes.FetchVerse)]
    //    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 3600)]
    //    public async Task<ApiResponse> GetVerseOfTheDay()
    //    {
    //        var spec = new VerseSpecification();

    //        var allVerses = await unit.Repository<VerseOfTheDayDataModel>().GetAllDataAsync(spec);

    //        // Get the length of all the verses
    //        if(allVerses.Count() == 0)
    //        {
    //            return new ApiResponse
    //            {
    //                ErrorMessage = "No verse at the moment"
    //            };
    //        }

    //        // Get a random number
    //        var randomNumber = new Random().Next(0, allVerses.Count());

    //        // Get a random verse
    //        var randomVerse = allVerses[randomNumber];

    //        // Return the random verse
    //        return new ApiResponse
    //        {
    //            Result = randomVerse
    //        };
    //    }

    //    /// <summary>
    //    /// Endpoint to delete a verse
    //    /// </summary>
    //    /// <param name="verseId"></param>
    //    /// <returns></returns>
    //    [HttpDelete(ApiRoutes.DeleteVerse)]
    //    [Authorize(Policy = "RequireAdminClaim")]
    //    public async Task<ApiResponse> DeleteVerse([FromQuery] int verseId)
    //    {
    //        // Create new spec
    //        var spec = new VerseSpecification(verseId);

    //        // Get the verse
    //        var verse = await unit.Repository<VerseOfTheDayDataModel>().GetDataWithSpec(spec);

    //        // If verse is null
    //        if (verse is null)
    //        {
    //            // Set the status code
    //            Response.StatusCode = (int)HttpStatusCode.NotFound;

    //            return new ApiResponse
    //            {
    //                ErrorMessage = "Verse not found"
    //            };
    //        }

    //        // Delete the verse
    //        unit.Repository<VerseOfTheDayDataModel>().DeleteData(verse);

    //        // Save changes
    //        await unit.Complete();

    //        // Return the response
    //        return new ApiResponse
    //        {
    //            Result = new { Message = "Verse deleted" }
    //        };
    //    }

    //    /// <summary>
    //    /// Endpoint to fetch all verses
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet(ApiRoutes.FetchVerses)]
    //    [Authorize(Policy = "RequireAdminClaim")]
    //    public async Task<ApiResponse> FetchAllVerses()
    //    {
    //        // Create a new spec
    //        var spec = new VerseSpecification();

    //        // Get all the verses
    //        var verses = await unit.Repository<VerseOfTheDayDataModel>().GetAllDataAsync(spec);

    //        // Return it
    //        return new ApiResponse
    //        {
    //            Result = verses
    //        };
    //    }     

    //    /// <summary>
    //    /// Endpoint to add verses
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    [HttpPost(ApiRoutes.AddVerses)]
    //    [Authorize(Policy = "RequireAdminClaim")]
    //    public async Task<ApiResponse> AddVerses(List<VerseOfTheDayApiModel> model)
    //    {
    //        // Create list of verse of the day api model
    //        List<VerseOfTheDayDataModel> verses = new();

    //        foreach(var verse in model)
    //        {
    //            var verseData = new VerseOfTheDayDataModel
    //            {
    //                Book = verse.Book,
    //                Passage = verse.Passage,
    //            };

    //            verses.Add(verseData);
    //        }

    //        await unit.Repository<VerseOfTheDayDataModel>().AddDataRange(verses);

    //        await unit.Complete();

    //        return new ApiResponse
    //        {
    //            Result = new { Message = "Verses added" }
    //        };
    //    }

    //    /// <summary>
    //    /// Endpoint to get scriptures for quiz questions
    //    /// </summary>
    //    /// <param name="model"></param>
    //    /// <returns></returns>
    //    [HttpPost(ApiRoutes.GetScriptureForQuestion)]
    //    public async Task<ApiResponse> GetScriptureForQuestion(BibleVerseApiModel model)
    //    {
    //        var (bookName, chapter, verse, errorMessage) = Verses.ExtractBibleVerse(model.BibleVerse);

    //        if(!string.IsNullOrWhiteSpace(errorMessage) || bookName is null || chapter is null || verse is null)
    //        {
    //            return new ApiResponse<BibleApiVerseSearchResponse, object, object>
    //            {
    //                Result = new BibleApiVerseSearchResponse { Data = new Data(), Meta = new Meta() },
    //                ErrorMessage = errorMessage ?? "An error occurred"
    //            };
    //        }

    //        var spec = new BibleBooksSpecification();

    //        var bibleBooks = await unit.Repository<BibleBooksDataModel>().GetAllDataAsync(spec);

    //        if(bibleBooks.Count > 0)
    //        {
    //            foreach(var book in bibleBooks)
    //            {
    //                var firstLetter = bookName[0].ToString();

    //                if (int.TryParse(firstLetter, out _))
    //                {
    //                    var bookNumber = int.TryParse(book.ShortName[0].ToString(), out _);

    //                    var numberMatches = bookNumber && firstLetter == book.ShortName[0].ToString();

    //                    if (numberMatches && book.ShortName.ToLower()[1].Equals(bookName.ToLower()[1]))
    //                    {
    //                        var matchingLetters = bookName.Count(x => book.ShortName.ToLower().Contains(x));

    //                        if (matchingLetters >= 2)
    //                        {
    //                            var response = await _bibleService.GetScripturesAsync(new BibleVerseApiModel
    //                            {
    //                                BibleVerse =
    //                                book.ShortNameUpper + "." + chapter + "." + verse
    //                            });

    //                            return response;

    //                        }

    //                    }
    //                }
    //                else
    //                {
    //                    if (book.ShortName.ToLower()[0].Equals(bookName.ToLower()[0]))
    //                    {
    //                        var matchingLetters = bookName.Count(x => book.ShortName.ToLower().Contains(x));

    //                        if (matchingLetters >= 2)
    //                        {
    //                            var response = await _bibleService.GetScripturesAsync(new BibleVerseApiModel
    //                            {
    //                                BibleVerse =
    //                                book.ShortNameUpper + "." + chapter + "." + verse
    //                            });

    //                            return response;

    //                        }


    //                    }
    //                }

    //            }
    //        }

    //        return new ApiResponse<BibleApiVerseSearchResponse, object, object>
    //        {
    //            Result = new BibleApiVerseSearchResponse { Data = new Data(), Meta = new Meta() },
    //            ErrorMessage = "Something went wrong"
    //        };

    //    }

    //    /// <summary>
    //    /// Endpoint to add Bible Books to Db
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet(ApiRoutes.AddBibleBooksToDb)]
    //    [Authorize(Policy = "RequireAdminClaim")]
    //    public async Task<ApiResponse> AddBibleBookNames()
    //    {
    //        // Send the request to Bible Api
    //        var response = await _bibleService.GetBiblelBookNames();

    //        // If it is successful
    //        if (response.Successful)
    //        {
    //            // Create a list of BibleBooks
    //            List<BibleBooksDataModel> bibleBooks = new();

    //            // Iterate over the data
    //            foreach(var book in response.Result.Data)
    //            {
    //                // Create instance of BibleBooksDataModel
    //                var bookToAdd = new BibleBooksDataModel
    //                {
    //                    BibleId = book.BibleId,
    //                    ShortName = book.Abbreviation,
    //                    ShortNameUpper = book.Id,
    //                    LongName = book.Name
    //                };

    //                // Add it to the list
    //                bibleBooks.Add(bookToAdd);
    //            }

    //            // Add the list to the db
    //            await unit.Repository<BibleBooksDataModel>().AddDataRange(bibleBooks);

    //            // Save the changes
    //            await unit.Complete();

    //            // Return the response
    //            return new ApiResponse
    //            {
    //                Result = "Data populated"
    //            };
    //        }

    //        // If it fails
    //        return response;
    //    }

    //    /// <summary>
    //    /// Endpoint to get Bible book names
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet(ApiRoutes.GetBibleBookNames)]
    //    [Authorize(Policy = "RequireAdminClaim")]
    //    public async Task<ApiResponse> GetBibleBookNames()
    //    {
    //        // Create a specification
    //        var spec = new BibleBooksSpecification();

    //        // Fetch the bible books from db
    //        var books = await unit.Repository<BibleBooksDataModel>().GetAllDataAsync(spec);

    //        // Initialize list of BibleBooksApiModel
    //        List<BibleBooksApiModel> booksToReturn = new();

    //        // Iterate over the books from db
    //        foreach(var book in books)
    //        {
    //            // Create instance of BibleBooksApiModel
    //            var bookToReturn = new BibleBooksApiModel
    //            {
    //                Id = book.Id,
    //                ShortName = book.ShortName,
    //                ShortNameUpper = book.ShortNameUpper,
    //                LongName = book.LongName,
    //                BibleId = book.BibleId
    //            };

    //            // Add to book to return
    //            booksToReturn.Add(bookToReturn);
    //        }

    //        // Return the result
    //        return new ApiResponse
    //        {
    //            Result = booksToReturn
    //        };

    //    }


    //    private async Task<ApiResponse<BibleApiVerseSearchResponse, object, object>> 
    //        CheckPattern(string bookName, BibleBooksDataModel book, string chapter, string verse)
    //    {
    //        var matchingLetters = bookName.Count(x => book.ShortName.ToLower().Contains(x));

    //        if (matchingLetters >= 2)
    //        {
    //            var response = await _bibleService.GetScripturesAsync(new BibleVerseApiModel
    //            {
    //                BibleVerse =
    //                book.ShortNameUpper + "." + chapter + "." + verse
    //            });

    //            return response;

    //        }
    //        else
    //        {
    //            return new ApiResponse<BibleApiVerseSearchResponse, object, object>
    //            {

    //            };
    //        }
    //    }
    //}
}
