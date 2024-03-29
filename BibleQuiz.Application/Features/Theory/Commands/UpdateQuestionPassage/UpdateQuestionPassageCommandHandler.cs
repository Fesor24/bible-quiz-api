﻿using BibleQuiz.Application.Errors;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using BibleQuiz.Domain.Specifications;
using MediatR;
using TestQuestionEntity = BibleQuiz.Domain.Entities.Theory;

namespace BibleQuiz.Application.Features.Theory.Commands.UpdateQuestionPassage;
internal sealed class UpdateQuestionPassageCommandHandler :
    IRequestHandler<UpdateQuestionPassageCommand, Result<Unit, Error>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateQuestionPassageCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit, Error>> Handle(UpdateQuestionPassageCommand request, CancellationToken cancellationToken)
    {
        var spec = new GetTestQuestionByIdSpecification(request.QuestionId);

        var question = await _unitOfWork.Repository<TestQuestionEntity>().GetAsync(spec);

        if (question is null)
            return ApplicationError.TestQuestion.NotFound(request.QuestionId);

        var res = question.UpdatePassage(request.Passage);

        if (res.IsFailure)
            return res.Error;

        _unitOfWork.Repository<TestQuestionEntity>().Update(res.Value);

        await _unitOfWork.Complete();

        return Unit.Value;
    }
}
