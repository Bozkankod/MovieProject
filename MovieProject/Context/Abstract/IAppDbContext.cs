using Microsoft.EntityFrameworkCore;
using MovieProject.Entitites;

namespace MovieProject.Context.Abstract
{
    public interface IAppDbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Vote> Votes { get; set; }
    }


}
