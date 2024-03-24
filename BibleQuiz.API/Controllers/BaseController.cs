using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers;

[ApiController]
public class BaseController<TController> : ControllerBase
{
    private ISender _sender;
    private IMapper _mapper;

    protected ISender Sender => _sender ??= 
        HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IMapper Mapper => _mapper ??= 
        HttpContext.RequestServices.GetRequiredService<IMapper>();
}
