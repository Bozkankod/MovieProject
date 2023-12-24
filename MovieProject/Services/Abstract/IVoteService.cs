using MovieProject.Entitites;

namespace MovieProject.Services.Abstract
{
    public interface IVoteService
    {
        Task<Vote> Add(Vote vote);
        Task<List<Vote>> GetVote(int MovieId);
    }
}
