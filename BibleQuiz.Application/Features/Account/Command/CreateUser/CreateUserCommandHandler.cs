using BibleQuiz.Domain.Services;
using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Account.Command.CreateUser;
internal sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<CreateUserResponse, Error>>
{
    private readonly ISecurityService _securityService;

    public CreateUserCommandHandler(ISecurityService securityService)
    {
        _securityService = securityService;
    }

    public async Task<Result<CreateUserResponse, Error>> Handle(CreateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var res = await _securityService.CreateUserAsync(request.UserName, request.Password);

        if (res.IsFailure)
            return res.Error;

        return new CreateUserResponse(request.UserName, res.Value);
    }
}
