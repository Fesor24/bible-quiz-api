using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Account.Command.CreateUser;
public record CreateUserCommand(string UserName, string Password) : 
    IRequest<Result<CreateUserResponse, Error>>;
