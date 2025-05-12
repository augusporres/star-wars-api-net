using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.Commons.Features.Commands.RegisterUser;
using MoviesProject.WebApi.Dtos.Users;
using MoviesProject.WebApi.Examples.RegisterUser;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController(
    IMediator mediator
) : ControllerBase
{
    private readonly IMediator _Mediator = mediator;
    /// <summary>
    /// Endpoint para registrar un nuevo usuario
    /// </summary>
    /// <param name="userCreationDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status200OK, "Usuario registrado exitosamente")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Error al registrar el usuario")]
    [SwaggerRequestExample(typeof(UserCreationDto), typeof(RegisterUserRequestExample))]

    public async Task<IActionResult> RegisterAsync([FromBody] UserCreationDto userCreationDto)
    {
        var command = new RegisterUserCommand(userCreationDto.Username, userCreationDto.Password);
        var result = await _Mediator.Send(command);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return BadRequest(result.Error);
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
