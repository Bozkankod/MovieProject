using MovieProject.Services.Abstract;

public class RecurringJobsManager
{
    private readonly IMovieService _movieService;

    public RecurringJobsManager(IMovieService databaseOptionService)
    {
        _movieService = databaseOptionService;
    }

    public async Task Process()
    {
        var result = await _movieService.FetchMoviesFromApiAsync();
        if (result != null)
        {
            foreach (var item in result.Movies)
            {
                var movie = await _movieService.GetById(item.Id);
                if (movie != null)
                {

                    continue;
                }
                await _movieService.Add(item);
            }


        }
    }
}