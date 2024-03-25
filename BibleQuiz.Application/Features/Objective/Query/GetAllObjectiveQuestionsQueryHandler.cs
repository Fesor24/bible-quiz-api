using AutoMapper;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleQuiz.Application.Features.Objective.Query;
internal sealed class GetAllObjectiveQuestionsQueryHandler : 
    IRequestHandler<GetAllObjectiveQuestionsQuery, Result<IReadOnlyList<ObjectiveQuestionResponse>, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllObjectiveQuestionsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<ObjectiveQuestionResponse>, Error>> Handle(GetAllObjectiveQuestionsQuery request,
        CancellationToken cancellationToken)
    {
        var objectives = await _unitOfWork.Repository<ObjectiveEntity>().GetAllAsync();

        var res = _mapper.Map<IReadOnlyList<ObjectiveQuestionResponse>>(objectives);

        return new Result<IReadOnlyList<ObjectiveQuestionResponse>, Error>(res);
    }
        
}
