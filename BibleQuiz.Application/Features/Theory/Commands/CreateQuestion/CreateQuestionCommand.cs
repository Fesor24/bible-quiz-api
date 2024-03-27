using BibleQuiz.Domain.Shared;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreateQuestion;
public class CreateQuestionCommand : IRequest<Result<TheoryEntity, Error>>
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int VerseFrom { get; set; }
    public int VerseTo { get; set;}
}
