using System;
using Hygie.Core.Entities.Base;

namespace Hygie.Core.Entities
{
    public class PatientExitDate : BaseEntity
    {
        public string UserId { get; set; }

        public DateTime ExitDate { get; set; }
    }
}

