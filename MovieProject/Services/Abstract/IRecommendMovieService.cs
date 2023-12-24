using MovieProject.Models.Requests;

namespace MovieProject.Services.Abstract
{
    public interface IRecommendMovieService
    {
        void Send(SendRecommendMovie model);
    }
}
