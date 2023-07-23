using Dapper;
using Hygie.Core.Entities;
using Hygie.Core.Repositories.Query;
using Hygie.Infrastructure.Repositories.Query.Base;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Hygie.Infrastructure.Repositories.Query
{
    public class PatientQueryRepository : QueryRepository<Patient>, IPatientQueryRepository
    {
        public PatientQueryRepository(IConfiguration configuration)
            : base(configuration)
        {

        }

        public async Task<IReadOnlyList<Patient>> GetAllAsync()
        {
            try
            {
                var query = "SELECT * FROM CUSTOMERS";

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryAsync<Patient>(query)).ToList();
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Patient> GetByIdAsync(long id)
        {
            try
            {
                var query = "SELECT * FROM CUSTOMERS WHERE Id = @Id";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Int64);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Patient>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }

        public async Task<Patient> GetPatientByEmail(string email)
        {
            try
            {
                var query = "SELECT * FROM CUSTOMERS WHERE Email = @email";
                var parameters = new DynamicParameters();
                parameters.Add("Email", email, DbType.String);

                using (var connection = CreateConnection())
                {
                    return (await connection.QueryFirstOrDefaultAsync<Patient>(query, parameters));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message, exp);
            }
        }
    }
}
