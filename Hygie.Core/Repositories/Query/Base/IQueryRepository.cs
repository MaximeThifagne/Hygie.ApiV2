using System.Linq.Expressions;

namespace Hygie.Core.Repositories.Query.Base
{
    public interface IQueryRepository<T> where T : class
    {
        // Generic repository for all if any
        Task<T?> GetAsync(string id);
        Task<T?> FindAsync(Expression<Func<T,bool>>predicate);
        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    }
}