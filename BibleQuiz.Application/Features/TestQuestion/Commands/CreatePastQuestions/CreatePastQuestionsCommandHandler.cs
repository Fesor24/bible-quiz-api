using TestQuestions = BibleQuiz.Domain.Entities.TestQuestion;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;
using BibleQuiz.Domain.Entities;

namespace BibleQuiz.Application.Features.TestQuestion.Commands.CreatePastQuestions;
internal sealed class CreatePastQuestionsCommandHandler : 
    IRequestHandler<CreatePastQuestionsCommand, Result<List<TestQuestions>, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePastQuestionsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<TestQuestions>, Error>> Handle(CreatePastQuestionsCommand request, 
        CancellationToken cancellationToken)
    {
        List<TestQuestions> questions = new();

        List<BibleBook> bibleBooks = await _unitOfWork.Repository<BibleBook>().GetAllAsync();

        foreach(var question in request.Questions)
        {
            var strippedAnswer = question.Answer.Split('(');

            string answer = string.Empty;

            string verse = string.Empty;

            if(strippedAnswer.Length > 1)
            {
                answer = strippedAnswer[0].Trim();
                verse = strippedAnswer[1].Replace(")", "").Trim();
            }
            else
            {
                answer = strippedAnswer[0].Trim();

                if(answer.Length < 15)
                    verse = strippedAnswer[0].Trim();
            }

            verse = FormatScriptureVerse(verse, bibleBooks);

            TestQuestions testQuestion = new(question.Question, answer, 
                request.Source, verse);

            questions.Add(testQuestion);
        }

        await _unitOfWork.Repository<TestQuestions>().AddRangeAsync(questions);

        await _unitOfWork.Complete();

        return questions;
    }


    private static string FormatScriptureVerse(string verse, List<BibleBook> books)
    {
        if (string.IsNullOrWhiteSpace(verse))
            return string.Empty;

        verse = verse.Replace(" ", "");

        string book = string.Empty;
        string location = string.Empty;

        for(int i = 0; i < verse.Length; i++)
        {
            if(i != 0 && int.TryParse(verse[i].ToString(), out _))
            {
                book = verse[..i];
                location = verse[i..];

                break;
            }
        }

        string bookSubStr = book.Length > 3 ? book.ToLower()[..4] : book.ToLower();

        var bibleBook = books.FirstOrDefault(x => x.LongName.ToLower().Replace(" ", "").Contains(bookSubStr));

        if (bibleBook is null)
            return string.Empty;

        return bibleBook.ShortName + "." + location.Replace(":", ".");
    }
}
