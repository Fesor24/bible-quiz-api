using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Account.Query.Login;
internal sealed class LoginRequestHandler : IRequestHandler<LoginRequest, Result<LoginResponse, Error>>
{
    private readonly ISecurityService _securityService;

    public LoginRequestHandler(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    public async Task<Result<LoginResponse, Error>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var res = await _securityService.CreateTokenAsync(request.UserName, request.Password);

        if (res.IsFailure)
            return res.Error;

        return new LoginResponse(request.UserName, res.Value);
    }
}
