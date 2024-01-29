using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.BibleBooks.Command.CreateBibleBooks;
public class CreateBibleBooksCommand : IRequest<Result<Unit, Error>>
{
}
