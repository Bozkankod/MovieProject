using Hangfire;

namespace MovieProject.Schedules
{
    public class RecurringJobs
    {
        public static void Add()
        {
            RecurringJob.RemoveIfExists("FetchMoviesAndSave");
            DateTime startTime = DateTime.Now;
            RecurringJob.AddOrUpdate<RecurringJobsManager>(
                "FetchMoviesAndSave",
                x => x.Process(),
                $"{startTime.Minute} * * * *"
            );
        }
    }
}
