using AutoMapper;
using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.BibleBooks.Query.GetBibleBooks;
internal sealed class GetBibleBooksRequestHandler : IRequestHandler<GetBibleBooksRequest, 
    Result<List<GetBibleBooksResponse>, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBibleBooksRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetBibleBooksResponse>, Error>> Handle(GetBibleBooksRequest request, 
        CancellationToken cancellationToken)
    {
        var books = await _unitOfWork.Repository<BibleBook>().GetAllAsync();

        return _mapper.Map<List<GetBibleBooksResponse>>(books);
    }
}
