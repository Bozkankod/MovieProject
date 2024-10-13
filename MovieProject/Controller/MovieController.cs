using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Caching.Abstract;
using MovieProject.Controller.Base;
using MovieProject.Entitites;
using MovieProject.Models.Requests;
using MovieProject.Services.Abstract;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MovieController : BaseController
{

    private readonly IMovieService _movieService;
    private readonly ICacheService _cacheService;
    private readonly IVoteService _voteService;

    public MovieController(IMovieService movieService, ICacheService cacheservice, IVoteService voteService)
    {
        _movieService = movieService;
        _cacheService = cacheservice;
        _voteService = voteService;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int pageNumber, int pageSize)
    {
        int skip = (pageNumber - 1) * pageSize;
        var moviesJson = _movieService.Get(skip, pageSize);
        if (moviesJson == null)
        {
            return BadRequest(new { Errors = "Movie not found" });
        }
        foreach (var item in moviesJson)
        {
            bool isSet = _cacheService.IsSet($"Movie_{item.Id}");
            if (!isSet)
            {
                _cacheService.Set($"Movie_{item.Id}", item, TimeSpan.FromHours(60));
            }
        }
        return Ok(moviesJson);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movieVote = await _voteService.GetVote(id);
        bool isSet = _cacheService.IsSet($"Movies_{id}");
        Movie movieResult = new();
        if (isSet)
        {
            var moviesJsonCached = _cacheService.Get<Movie>($"Movies_{id}");
            movieResult = new Movie
            {
                Id = moviesJsonCached.Id,
                Title = moviesJsonCached.Title,
                VoteAverage = moviesJsonCached.VoteAverage,
                VoteCount = moviesJsonCached.VoteCount,
                Votes = movieVote
            };
            return Ok(movieResult);
        }
        var moviesJson = await _movieService.GetById(id);
        if (moviesJson == null)
        {
            return BadRequest(new { Errors = "Movie not found" });
        }
        movieResult = new Movie
        {
            Id = moviesJson.Id,
            Title = moviesJson.Title,
            VoteAverage = moviesJson.VoteAverage,
            VoteCount = moviesJson.VoteCount,
            Votes = movieVote
        };
        return Ok(moviesJson);
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddMovieModel model)
    {

        Movie movie = new()
        {
            Id = model.Id,
            VoteCount = model.VoteCount,
            Title = model.Title,
            VoteAverage = model.VoteAverage
        };

        var result = await _movieService.Add(movie);
        if (result == null)
        {
            return BadRequest(new { Errors = "Movie was not added" });
        }
        _cacheService.Set($"Movie_{movie.Id}", movie, TimeSpan.FromHours(60));
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(RemoveMovieModel model)
    {

        Movie movie = new()
        {
            Id = model.MovieId,
        };

        var result = await _movieService.Remove(movie);
        if (result == null)
        {
            return BadRequest(new { Errors = "Movie was not deleted" });
        }
        _cacheService.Remove($"Movie_{movie.Id}");
        return Ok(result);
    }

    [HttpPatch]
    public IActionResult Update(UpdateMovieModel model)
    {

        Movie movie = new()
        {
            Id = model.Id,
            VoteCount = model.VoteCount,
            Title = model.Title,
            VoteAverage = model.VoteAverage
        };

        var result = _movieService.Update(movie);
        if (result == null)
        {
            return BadRequest(new { Errors = "Movie was not updated" });
        }
        _cacheService.Set($"Movie_{movie.Id}", movie, TimeSpan.FromHours(60));
        return Ok(result);
    }

}