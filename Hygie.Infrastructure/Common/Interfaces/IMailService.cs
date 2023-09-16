using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygie.Infrastructure.Common.Interfaces
{
    public interface IMailService
    {
        Task SendResetPasswordLink(string email);
    }
}
