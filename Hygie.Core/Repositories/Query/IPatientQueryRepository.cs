using Hygie.Core.Entities;
using Hygie.Core.Repositories.Query.Base;

namespace Hygie.Core.Repositories.Query
{
    public interface IPatientQueryRepository : IQueryRepository<Patient>
    {
        //Custom operation which is not generic
        Task<IReadOnlyList<Patient>> GetAllAsync();
        Task<Patient> GetByIdAsync(Int64 id);
        Task<Patient> GetPatientByEmail(string email);
    }
}