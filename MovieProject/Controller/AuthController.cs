using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MovieProject.Controller.Base;
using MovieProject.Models.Requests;
using MovieProject.Services.Abstract;

[AllowAnonymous]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }


    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] AuthLoginModel model)
    {
        var data = await _authService.Login(model);

        if (data == null)
        {
            return BadRequest(new { Errors = "Invalid username or password" });
        }

        return Ok(data);
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthLoginModel model)
    {

        var result = await _authService.Register(model);

        if (result == null)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}
