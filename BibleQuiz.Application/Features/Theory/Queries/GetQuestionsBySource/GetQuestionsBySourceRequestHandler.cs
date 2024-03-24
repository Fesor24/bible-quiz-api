using AutoMapper;
using TestQuestions = BibleQuiz.Domain.Entities.Theory;
using BibleQuiz.Domain.Primitives;
using BibleQuiz.Domain.Shared;
using MediatR;
using BibleQuiz.Domain.Specifications;
using BibleQuiz.Application.Features.Theory.Queries.GetQuestions;

namespace BibleQuiz.Application.Features.Theory.Queries.GetQuestionsBySource;
internal sealed class GetQuestionsBySourceRequestHandler : IRequestHandler<GetQuestionsBySourceRequest,
    Result<IReadOnlyList<GetQuestionResponse>, Error>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetQuestionsBySourceRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<GetQuestionResponse>, Error>> Handle(GetQuestionsBySourceRequest request,
        CancellationToken cancellationToken)
    {
        var spec = new GetTestQuestionsBySourceSpecification(request.Source);

        var res = await _unitOfWork.Repository<TestQuestions>().GetAllWithSpecAsync(spec);

        if (res is null)
            return new List<GetQuestionResponse>();

        return _mapper.Map<List<GetQuestionResponse>>(res);
    }

}
