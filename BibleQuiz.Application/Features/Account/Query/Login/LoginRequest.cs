using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Account.Query.Login;
public record LoginRequest(string UserName, string Password) : IRequest<Result<LoginResponse, Error>>;
