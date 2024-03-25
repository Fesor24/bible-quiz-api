using BibleQuiz.Domain.Shared;
using FluentValidation;
using MediatR;

namespace BibleQuiz.Application.Features.Theory.Commands.CreateQuestions;
public class CreateQuestionsCommand : IRequest<Result<Unit, Error>>
{
    public List<CreateQuestion> Questions { get; set; }
}

public class CreateQuestionsCommandValidator : AbstractValidator<CreateQuestionsCommand>
{
    public CreateQuestionsCommandValidator()
    {
        RuleFor(x => x.Questions)
            .NotEmpty()
            .WithMessage("Add at least one question");

        RuleFor(x => x.Questions)
            .NotNull()
            .WithMessage("Questions can not be null");

        RuleForEach(x => x.Questions)
            .ChildRules(question =>
            {
                question.RuleFor(q => q.Question)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Question can not be null or empty");

                question.RuleFor(q => q.Answer)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Answer can not be null or empty");

                question.RuleFor(q => q.Verse)
                    .NotNull()
                    .NotEmpty()
                    .WithMessage("Verse can not be null or empty");
            });
    }
}

public class CreateQuestion
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Verse { get; set; }
}
