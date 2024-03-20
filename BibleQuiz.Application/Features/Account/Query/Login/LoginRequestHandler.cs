using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Account.Query.Login;
internal sealed class LoginRequestHandler : IRequestHandler<LoginRequest, Result<LoginResponse, Error>>
{
    public Task<Result<LoginResponse, Error>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
