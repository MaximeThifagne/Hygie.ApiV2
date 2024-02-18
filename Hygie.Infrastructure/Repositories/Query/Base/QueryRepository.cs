using System.Linq.Expressions;
using Hygie.Core.Repositories.Query.Base;
using Hygie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Hygie.Infrastructure.Repositories.Query.Base
{
    public class QueryRepository<T> :  IQueryRepository<T> where T : class
    {
        protected readonly HygieContext _context;

        public QueryRepository(HygieContext context)
        {
            _context = context;
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<List<T>>FindAllAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().Where(predicate).ToListAsync();

        public async Task<T?> GetAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
