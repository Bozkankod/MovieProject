using MovieProject.Entitites;
using MovieProject.Exceptions;
using MovieProject.Models.Requests;
using MovieProject.Models.Responses;

namespace MovieProject.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResultLogin> Login(AuthLoginModel login);
        Task<ApiException> Register(AuthLoginModel auth);
    }
}
