using Microsoft.AspNetCore.Mvc;
using MoviesProject.WebApi.Dtos;

namespace MoviesProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] UserCreationDto userCreationDto)
    {
        return Ok();
        // var command = new RegisterUserCommand(userCreationDto);
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return Ok(new UserCreationResponse());
        // }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto userLoginDto)
    {
        return Ok();
        // var command = new LoginUserCommand(userLoginDto);
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return Ok(new UserLoginResponse());
        // }
    }
}
