using MovieProject.Entitites;
using MovieProject.Repositories.Abstract;
using MovieProject.Services.Abstract;

namespace MovieProject.Services.Concrete
{
    public class VoteService : IVoteService
    {

        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository movieRepository)
        {
            _voteRepository = movieRepository;
        }

        public async Task<Vote> Add(Vote vote)
        {
            var result = await _voteRepository.AddAsync(vote);
            return result;
        }

        public Task<List<Vote>> GetVote(int MovieId)
        {
            var result = _voteRepository.GetWhereAsync(Vote => Vote.MovieId == MovieId);
            return result;
        }
    }
}
