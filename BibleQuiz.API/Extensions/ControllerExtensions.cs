using BibleQuiz.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Extensions;

internal static class ControllerExtensions
{
    internal static IActionResult HandleErrorResult(this ControllerBase controller, Error error)
    {
        if(error.GetType() == typeof(ValidationError))
            return controller.BadRequest(error);
        else if(error.GetType() == typeof(NotFoundError))
            return controller.NotFound(error);
        else
            return controller.BadRequest(error);
    }
}
