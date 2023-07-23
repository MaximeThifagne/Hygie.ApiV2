using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygie.Core.Repositories.Command
{
    public interface IPatientCommandRepository : ICommandRepository<Patient>
    {

    }
}
