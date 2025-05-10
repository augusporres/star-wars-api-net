using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesProject.Commons.Features.Queries.GetAllMovies;
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
        // if (result.IsSuccess)
        // {
        //     return Ok(result.Value);
        // }
        return NotFound();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieDetailsByIdAsync(int id)
    {
        // var query = new GetMovieDetailsByIdQuery(id);
        // var result = await _Mediator.Send(query);
        // if (result.IsSuccess)
        // {
        //     return Ok(result.Value);
        // }
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> CreateMovieAsync([FromBody] CreateMovieDto createMovieDto)
    {
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return CreatedAtAction(nameof(GetMovieDetailsByIdAsync), new { id = result.Value.Id }, result.Value);
        // }
        return BadRequest();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMovieAsync(int id, [FromBody] UpdateMovieDto updateMovieDto)
    {
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return NoContent();
        // }
        return NotFound();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        // var result = await _Mediator.Send(command);
        // if (result.IsSuccess)
        // {
        //     return NoContent();
        // }
        return NotFound();
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
