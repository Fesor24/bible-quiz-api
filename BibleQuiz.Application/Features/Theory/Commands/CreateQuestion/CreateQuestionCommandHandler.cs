using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreateQuestion;
internal sealed class CreateQuestionCommandHandler :
    IRequestHandler<CreateQuestionCommand, Result<int, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateQuestionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<int, Error>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var bibleBooks = await _unitOfWork.Repository<BibleBookEntity>().GetAllAsync();

        var book = bibleBooks.FirstOrDefault(x => x.ShortName.Equals(request.Book, StringComparison.OrdinalIgnoreCase));

        if (book is null)
            return Error.NotFound("Book.NotFound", $"Book with name:{request.Book} not found");

        string completeVerse = string.Empty;

        if (request.VerseTo != default)
            completeVerse = book.ShortName + "." + request.Chapter + "." +
                request.VerseFrom.ToString() + "-" + request.VerseTo.ToString();
        else
            completeVerse = book.ShortName + "." + request.Chapter + "." +
                request.VerseFrom.ToString();

        var res = TheoryEntity.Create(request.Question, request.Answer, Domain.Enums.QuestionSource.Author,
            completeVerse);

        if (res.IsFailure)
            return res.Error;

        await _unitOfWork.Repository<TheoryEntity>().AddAsync(res.Value);

        await _unitOfWork.Complete();

        return res.Value.Id;
    }
}
