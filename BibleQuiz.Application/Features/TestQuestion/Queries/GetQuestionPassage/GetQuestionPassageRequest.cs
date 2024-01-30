using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestionPassage;
public record GetQuestionPassageRequest(int QuestionId) : IRequest<Result<GetQuestionPassageResponse, Error>>;
