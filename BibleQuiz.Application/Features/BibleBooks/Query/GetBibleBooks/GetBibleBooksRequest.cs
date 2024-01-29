using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.BibleBooks.Query.GetBibleBooks;
public class GetBibleBooksRequest : IRequest<Result<List<GetBibleBooksResponse>, Error>>
{
}
