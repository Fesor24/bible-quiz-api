﻿using BibleQuiz.Domain.Entities;
using BibleQuiz.Domain.Enums;

namespace BibleQuiz.Domain.Specifications;
public class GetTestQuestionsBySourceSpecification : BaseSpecification<TestQuestion>
{
    public GetTestQuestionsBySourceSpecification(QuestionSource source) : base(x => x.Source == source)
    {
        SetOrderBy(x => x.Id);
    }
}
