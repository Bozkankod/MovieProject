using MovieProject.Entitites;
using MovieProject.Models.Responses;

namespace MovieProject.Services.Abstract
{
    public interface IMovieService
    {
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);
        Task<Movie> Remove(Movie movie);
        List<Movie> Get(int pageNumber, int pageSize);
        Task<Movie> GetById(int id);

        Task<ResultMovie> FetchMoviesFromApiAsync();
    }
}
