using MovieProject.Entitites.Base;
using System.Linq.Expressions;

namespace MovieProject.Repositories.Abstract.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        List<T> GetPagedList(int pageNumber, int pageSize);
        Task<List<T>> GetAsync();
        Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<T> AddAsync(T model);
        T Update(T model);
        Task<T> RemoveAsync(T model);

        Task<T> Login(string email, string password);

        Task<T> SendEmail(T model);

    }


}
