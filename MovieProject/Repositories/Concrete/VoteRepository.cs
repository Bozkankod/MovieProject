using MovieProject.Entitites;
using MovieProject.Repositories.Abstract;
using MovieProject.Repositories.Concrete.Base;

namespace MovieProject.Repositories.Concrete
{
    public class VoteRepository : BaseRepository<Vote>, IVoteRepository
    {
        public VoteRepository(AppDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }

}
