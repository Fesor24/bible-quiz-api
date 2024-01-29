using BibleQuiz.Application.Constants;
using BibleQuiz.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Extensions;

internal static class ControllerExtensions
{
    internal static IActionResult HandleErrorResult(this ControllerBase controller, Error error)
    {
        if (error.Code == ApplicationStatusCodes.NOTFOUND)
            return controller.NotFound(error);
        else
            return controller.BadRequest(error);
    }
}
