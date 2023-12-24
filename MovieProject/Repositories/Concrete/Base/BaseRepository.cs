using Microsoft.EntityFrameworkCore;
using MovieProject.Entitites.Base;
using MovieProject.Repositories.Abstract.Base;
using System.Linq.Expressions;
using X.PagedList;

namespace MovieProject.Repositories.Concrete.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _applicationDbContext;
        public BaseRepository(AppDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        private DbSet<T> Table { get => _applicationDbContext.Set<T>(); }
        public async Task<T> AddAsync(T model)
        {
            await Table.AddAsync(model);
            await _applicationDbContext.SaveChangesAsync();
            return model;
        }
        public async Task<List<T>> GetAsync()
        {
            return await Table.ToListAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await Table.FindAsync(id);
        }

        public T GetById(int id)
        {
            return Table.Find(id);
        }

        public async Task<List<T>> GetWhereAsync(Expression<Func<T, bool>> expression)
        {
            return await Table.Where(expression).ToListAsync();
        }
        public async Task<T> RemoveAsync(T model)
        {
            Table.Remove(model);
            await _applicationDbContext.SaveChangesAsync();
            return model;
        }

        public T Update(T model)
        {
            Table.Update(model);
            _applicationDbContext.SaveChanges();
            return model;
        }

        public List<T> GetPagedList(int pageNumber, int pageSize)
        {
            return Table.ToPagedList(pageNumber, pageSize).ToList();
        }

        public async Task<T> Login(string email, string password)
        {
            return await Table.FindAsync(email, password);

        }

        public async Task<T> Register(T model)
        {
            await Table.AddAsync(model);
            await _applicationDbContext.SaveChangesAsync();
            return model;
        }

        public async Task<T> SendEmail(T model)
        {
            await SendEmail(model);
            return model;
        }

        public async Task<T> AddMovies(T model)
        {
            await Table.AddAsync(model);
            await _applicationDbContext.SaveChangesAsync();
            return model;
        }

    }
}