using Microsoft.EntityFrameworkCore;
using MovieProject.Seed;

namespace MovieProject.Extensions
{
    public static class DbContextExtension
    {
        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            //Seed
            using var serviceScope = services.BuildServiceProvider().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
            context.Database.Migrate();

            var seedContext = serviceScope.ServiceProvider.GetService<RecurringJobsManager>();
            SeedMovie.SeedMovieAsync(seedContext).Wait();


        }
    }
}
