using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Controller.Base;
using MovieProject.Models.Requests;
using MovieProject.Services.Abstract;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class RecommendController : BaseController
{
    private readonly IRecommendMovieService _recommendMovieService;

    public RecommendController(IRecommendMovieService recommendMovieService)
    {
        _recommendMovieService = recommendMovieService;
    }


    [HttpPost]
    [Route("recommend-movie")]
    public async Task<IActionResult> RecommendMovie(SendRecommendMovie model)
    {
        _recommendMovieService.Send(model);
        return Ok("Your email has been sended.");
    }
}
