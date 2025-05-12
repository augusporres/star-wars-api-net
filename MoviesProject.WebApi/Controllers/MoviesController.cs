using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.Commons.Features.Commands.CreateMovie;
using MoviesProject.Commons.Features.Commands.DeleteMovie;
using MoviesProject.Commons.Features.Commands.SyncMovies;
using MoviesProject.Commons.Features.Commands.UpdateMovie;
using MoviesProject.Commons.Features.Queries.GetAllMovies;
using MoviesProject.Commons.Features.Queries.GetMovieDetails;
using MoviesProject.Commons.Models;
using MoviesProject.WebApi.Dtos.Movies;
using MoviesProject.WebApi.Examples.CreateMovie;
using MoviesProject.WebApi.Examples.DeleteMovie;
using MoviesProject.WebApi.Examples.GetAllMovies;
using MoviesProject.WebApi.Examples.GetMovieById;
using MoviesProject.WebApi.Examples.SyncMovies;
using MoviesProject.WebApi.Examples.UpdateMovie;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace MoviesProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _Mediator = mediator;

    /// <summary>
    /// Endpoint para recuperar todas las películas
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllMoviesSucessResponseExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró el recurso")]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "No tiene permisos para acceder a este recurso")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        var query = new GetAllMoviesQuery();
        var result = await _Mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error);
    }

    /// <summary>
    /// Endpoint para recuperar una película por su ID
    /// </summary>
    /// <param name="id">Id de la película. <b>Ejemplo: 1</b></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetMovieByIdSuccessResponseExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró la película")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetMovieDetailsByIdAsync(int id)
    {
        var query = new GetMovieDetailsByIdQuery(id);
        var result = await _Mediator.Send(query);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound();
    }

    /// <summary>
    /// Endpoint para crear una película
    /// </summary>
    /// <param name="createMovieDto"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status201Created)]
    [SwaggerRequestExample(typeof(CreateMovieDto), typeof(CreateMovieRequestExample))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Solicitud inválida")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDto createMovieDto)
    {
        var result = await _Mediator.Send(new CreateMovieCommand(
            createMovieDto.Title,
            createMovieDto.Episode,
            createMovieDto.Director,
            createMovieDto.Producer,
            createMovieDto.OpenningCrawl
        ));
        if (result.IsSuccess)
        {
            return Created();
        }
        return BadRequest(result.Error);
    }

    /// <summary>
    /// Endpoint para actualizar una película por id
    /// </summary>
    /// <param name="id">Id de la película. <b>Ejemplo: 1</b></param>
    /// <param name="updateMovieDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    [SwaggerRequestExample(typeof(UpdateMovieDto), typeof(UpdateMovieRequestExample))]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateMovieSuccessResponseExample))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró la película")]
    public async Task<IActionResult> UpdateMovieAsync(int id, [FromBody] UpdateMovieDto updateMovieDto)
    {
        var result = await _Mediator.Send(new UpdateMovieCommand(
            id,
            updateMovieDto.Title,
            updateMovieDto.Episode,
            updateMovieDto.OpenningCrawl,
            updateMovieDto.Director,
            updateMovieDto.Producer
        ));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error);
    }

    /// <summary>
    /// Endpoint para eliminar una película por id
    /// </summary>
    /// <param name="id">Id de la película. <b>Ejemplo: 1</b></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteMovieSuccessResponseExample))]
    // [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No se encontró la película")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        var result = await _Mediator.Send(new DeleteMovieCommand(id));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error);
    }

    /// <summary>
    /// Endpoint para sincronizar películas desde la API de Star Wars
    /// </summary>
    /// <returns></returns>
    [HttpPost("sync")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(SyncMoviesCommandResponse), StatusCodes.Status200OK)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(SyncMoviesSuccessResponseExample))]
    public async Task<IActionResult> SyncMoviesFromApiAsync()
    {
        var result = await _Mediator.Send(new SyncMoviesCommand());
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Value);
    }
}
