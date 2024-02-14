using System;
namespace Hygie.Infrastructure.Common.Interfaces
{
    public interface IPatientService
    {
        Task<bool> SetHospitalExitDateAsync(string userId, DateTime exitDate);
    }
}

