using BibleQuiz.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Extensions;

public static class ControllerExtensions
{
    public static IActionResult HandleErrorResult(this ControllerBase controller, Error error)
    {
        if (error.Code == "404")
            return controller.NotFound(error);
        else
            return controller.BadRequest(error);
    }
}
