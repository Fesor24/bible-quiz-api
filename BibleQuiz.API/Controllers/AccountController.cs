using BibleQuiz.API.Dtos;
using BibleQuiz.API.Extensions;
using BibleQuiz.Application.Features.Account.Command.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace BibleQuiz.API.Controllers
{
    public class AccountController : BaseController<AccountController>
    {
        [HttpPost("/api/user")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserDto model)
        {
            var res = await Sender.Send(new CreateUserCommand(model.UserName, model.Password));

            return res.Match(value => Ok(value),
                this.HandleErrorResult);
        } 
    }
}
