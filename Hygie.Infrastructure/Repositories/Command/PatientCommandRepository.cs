using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command;
using Hygie.Infrastructure.Data;
using Hygie.Infrastructure.Repositories.Command.Base;

namespace Hygie.Infrastructure.Repositories.Command
{

    public class PatientCommandRepository : CommandRepository<Patient>, IPatientCommandRepository
    {
        public PatientCommandRepository(HygieContext context) : base(context)
        {

        }
    }
}
