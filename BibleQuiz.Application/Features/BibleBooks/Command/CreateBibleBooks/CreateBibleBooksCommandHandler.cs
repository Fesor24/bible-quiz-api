using BibleBookEntity =  BibleQuiz.Domain.Entities.BibleBook;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.BibleBooks.Command.CreateBibleBooks;
internal sealed class CreateBibleBooksCommandHandler : IRequestHandler<CreateBibleBooksCommand, 
    Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBibleService _bibleService;

    public CreateBibleBooksCommandHandler(IUnitOfWork unitOfWork, IBibleService bibleService)
    {
        _unitOfWork = unitOfWork;
        _bibleService = bibleService;
    }

    public async Task<Result<Unit, Error>> Handle(CreateBibleBooksCommand request, CancellationToken cancellationToken)
    {
        var entityBibleBooks = await _unitOfWork.Repository<BibleBookEntity>().GetAllAsync();

        if (entityBibleBooks is not null && entityBibleBooks.Any())
            return Unit.Value;

        var res = await _bibleService.GetBibleBooks();

        if (res.IsFailure)
            return res.Error;

        var books = res.Value.Data;

        List<BibleBookEntity> bibleBooks = new();

        foreach(var book in books)
        {
            BibleBookEntity entity = new(book.BibleId, book.Abbreviation, book.Id, book.Name);

            bibleBooks.Add(entity);
        }

        await _unitOfWork.Repository<BibleBookEntity>().AddRangeAsync(bibleBooks);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
