using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Caching.Abstract;
using MovieProject.Controller.Base;
using MovieProject.Entitites;
using MovieProject.Models.Requests;
using MovieProject.Services.Abstract;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class VoteController : BaseController
{

    private readonly IVoteService _voteService;
    private readonly IMovieService _movieService;
    private readonly ICacheService _cacheService;

    public VoteController(IVoteService voteService, IMovieService movieService, ICacheService cacheService)
    {
        _voteService = voteService;
        _movieService = movieService;
        _cacheService = cacheService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddVoteModel model)
    {
        if(model.Point > 10 || model.Point < 1)
        {
            return BadRequest(new { Errors = "Vote point must be between 1 and 10." });
        }
        var userId = User.Identity.Name;

        Vote vote = new()
        {
            MovieId = model.MovieId,
            UserId = userId,
            Point = model.Point,
            Comment = model.Comment
        };

        var result = await _voteService.Add(vote);
        if (result == null)
        {
            return BadRequest(new { Errors = "Vote was not add" });
        }

        var movie = await _movieService.GetById(model.MovieId);
        if (movie != null)
        {
            movie.VoteCount++;
            movie.VoteAverage = (movie.VoteAverage + model.Point) / movie.VoteCount;

            var execute = _movieService.Update(movie);
            if (execute == null)
            {
                return BadRequest(new { Errors = "Movie was not update" });
            }
        }
        else
        {
            return BadRequest(new { Errors = "Movie was not found" });
        }
        _cacheService.Set($"Movie_{movie.Id}", movie, TimeSpan.FromHours(60));
        return Ok(result);
    }
}