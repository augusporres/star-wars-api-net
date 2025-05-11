using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.Commons.Features.Commands.CreateMovie;
using MoviesProject.Commons.Features.Commands.DeleteMovie;
using MoviesProject.Commons.Features.Commands.UpdateMovie;
using MoviesProject.Commons.Features.Queries.GetAllMovies;
using MoviesProject.Commons.Features.Queries.GetMovieDetails;
using MoviesProject.WebApi.Dtos.Movies;

namespace MoviesProject.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _Mediator = mediator;

    [HttpGet]
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

    [HttpGet("{id}")]
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
    [HttpPost]
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
    [HttpPut("{id}")]
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        var result = await _Mediator.Send(new DeleteMovieCommand(id));
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return NotFound(result.Error);
    }
    [HttpPost("sync")]
    public async Task<IActionResult> SyncMoviesFromApiAsync()
    {
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return Ok(result.Value);
        // }
        return NotFound();
    }
}
