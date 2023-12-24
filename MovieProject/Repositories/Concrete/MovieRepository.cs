using MovieProject.Entitites;
using MovieProject.Repositories.Abstract;
using MovieProject.Repositories.Concrete.Base;

namespace MovieProject.Repositories.Concrete
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(AppDbContext applicationDbContext) : base(applicationDbContext)
        {


        }


    }
}
