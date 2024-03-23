using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers;

[ApiController]
public class BaseController<TController> : ControllerBase
{
    private ISender _sender;

    protected ISender Sender => _sender ??= 
        HttpContext.RequestServices.GetService<ISender>();
}
