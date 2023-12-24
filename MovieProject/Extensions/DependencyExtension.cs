using Hangfire;
using Microsoft.AspNetCore.Identity;
using MovieProject.Caching.Abstract;
using MovieProject.Caching.Concrete.Redis;
using MovieProject.Email.Abstract;
using MovieProject.Repositories.Abstract;
using MovieProject.Repositories.Abstract.Base;
using MovieProject.Repositories.Concrete;
using MovieProject.Repositories.Concrete.Base;
using MovieProject.Services.Abstract;
using MovieProject.Services.Concrete;
using MovieProject.Settings;

namespace MovieProject.Extensions
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            //Configuration
            services.Configure<TheMovieDbSetting>(configuration.GetSection("TheMovieDB"));
            services.Configure<RedisSetting>(configuration.GetSection("RedisSettings"));
            var cacheConnectionString = configuration.GetSection("RedisSettings:RedisConnectionString");

            //Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            //Services   
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IVoteService, VoteService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IRecommendMovieService, RecommendMovieService>();

            //Cache Service  
            services.AddScoped<ICacheService, RedisCache>();
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = cacheConnectionString.Value;
            });

            //Email Service
            services.AddScoped<IEmailService, EmailService>();

            //Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
            services.AddScoped<UserManager<ApplicationUser>>();

            //Hangfire
            services.AddHangfire(x => x.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            //Job Manager
            services.AddScoped<RecurringJobsManager>();

        }
    }
}
