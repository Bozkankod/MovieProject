using MovieProject.Repositories.Abstract;

namespace MovieProject.Seed
{
    public static class SeedMovie
    {
        public static async Task SeedMovieAsync(RecurringJobsManager movieRepository)
        {
            await movieRepository.Process();
        }
    }
}
