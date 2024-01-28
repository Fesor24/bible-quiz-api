﻿using BibleQuiz.Domain.Enums;

namespace BibleQuiz.Application.Features.TestQuestion.Queries.GetQuestions;
public class GetQuestionResponse
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string Verse { get; set; }
    public QuestionSource Source { get; set; }
}