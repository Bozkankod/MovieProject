using Microsoft.AspNetCore.Identity;
using MovieProject.Exceptions;
using MovieProject.Helpers;
using MovieProject.Models.Requests;
using MovieProject.Models.Responses;
using MovieProject.Services.Abstract;
using System.Net;

namespace MovieProject.Services.Concrete
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApiException> Register(AuthLoginModel register)
        {

            var user = new ApplicationUser
            {
                Email = register.Email,
                UserName = register.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                return new ApiException($"User was not created. {result.Errors}") { StatusCode = (int)HttpStatusCode.BadRequest };
            }


            return new ApiException($"User has been created {result.Errors}") { StatusCode = (int)HttpStatusCode.OK };

        }

        public async Task<ResultLogin> Login(AuthLoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email.Trim()) ?? throw new ApiException($"You are not registered with '{login.Email}'.") { StatusCode = (int)HttpStatusCode.BadRequest };
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(user, login.Password, false, lockoutOnFailure: false);

            if (!signInResult.Succeeded)
            {
                throw new ApiException($"Invalid Credentials for '{login.Email}'.") { StatusCode = (int)HttpStatusCode.BadRequest };
            }

            var token = JwtTokenHelper.GenerateJwtToken(user);

            ResultLogin resultLogin = new() { Email = user.Email, Token = token };


            return resultLogin;
        }
    }
}
