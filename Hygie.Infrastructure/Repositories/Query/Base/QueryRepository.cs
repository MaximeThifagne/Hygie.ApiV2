using Hygie.Core.Repositories.Query.Base;
using Hygie.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Hygie.Infrastructure.Repositories.Query.Base
{
    public class QueryRepository<T> : DbConnector, IQueryRepository<T> where T : class
    {
        public QueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }
    }
}
