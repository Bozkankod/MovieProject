using Microsoft.Extensions.Options;
using MovieProject.Caching.Abstract;
using MovieProject.Entitites;
using MovieProject.Models.Responses;
using MovieProject.Repositories.Abstract;
using MovieProject.Services.Abstract;
using MovieProject.Settings;
using Newtonsoft.Json;

namespace MovieProject.Services.Concrete
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ICacheService _cacheService;

        private readonly TheMovieDbSetting _theMovieDbSetting;

        public MovieService(IOptions<TheMovieDbSetting> theMovieDbSetting, IMovieRepository movieRepository, ICacheService cacheService)
        {
            _theMovieDbSetting = theMovieDbSetting.Value;
            _movieRepository = movieRepository;
            _cacheService = cacheService;
        }


        public async Task<Movie> Add(Movie movie)
        {
            var result = await _movieRepository.AddAsync(movie);
            return result;
        }

        public List<Movie> Get(int pageNumber, int pageSize)
        {
            var result = _movieRepository.GetPagedList(pageNumber, pageSize);
            return result;
        }



        public async Task<Movie> GetById(int id)
        {
            var result = await _movieRepository.GetByIdAsync(id);
            return result;
        }

        public async Task<Movie> Remove(Movie movie)
        {
            var result = await _movieRepository.RemoveAsync(movie);
            return result;
        }

        public Movie Update(Movie movie)
        {
            var result = _movieRepository.Update(movie);
            return result;
        }

        public async Task<ResultMovie> FetchMoviesFromApiAsync()
        {
            const int count = 50;
            int page = 1;
            var outSourceUrl = _theMovieDbSetting.ApiUrl;
            var apiKey = _theMovieDbSetting.ApiKey;

            var resultMovies = new ResultMovie();

            using (var httpClient = new HttpClient())
            {
                while (page <= count)
                {
                    var url = $"{outSourceUrl}/discover/movie?api_key={apiKey}&page={page}";

                    try
                    {
                        using var response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        var jsonContent = await response.Content.ReadAsStringAsync();

                        if (string.IsNullOrEmpty(jsonContent))
                        {
                            break;
                        }

                        var formattedResult = JsonConvert.DeserializeObject<ResultMovie>(jsonContent);

                        if (formattedResult == null || formattedResult.Movies == null || formattedResult.Movies.Count == 0)
                        {
                            break;
                        }

                        resultMovies.Movies.AddRange(formattedResult.Movies);
                        foreach (var item in formattedResult.Movies)
                        {
                            bool isCache = _cacheService.IsSet($"Movie_{item.Id}");
                            if (!isCache)
                                _cacheService.Set($"Movie_{item.Id}", item, TimeSpan.FromHours(1));
                        }
                        page++;
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        break;
                    }
                }
            }


            return resultMovies;
        }
    }
}
