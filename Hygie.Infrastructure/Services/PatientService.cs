using System;
using System.Linq.Expressions;
using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command.Base;
using Hygie.Core.Repositories.Query.Base;
using Hygie.Infrastructure.Common.Interfaces;

namespace Hygie.Infrastructure.Services
{
    public class PatientService : IPatientService
    {
        private readonly IQueryRepository<PatientExitDate> _patientExitDateQueryRepository;
        private readonly ICommandRepository<PatientExitDate> _patientExitDateCommandRepository;

        public PatientService(IQueryRepository<PatientExitDate> patientExitDateQueryRepository, ICommandRepository<PatientExitDate> patientExitDateCommandRepository)
        {
            _patientExitDateQueryRepository = patientExitDateQueryRepository;
            _patientExitDateCommandRepository = patientExitDateCommandRepository;
        }

        public async Task<bool> SetHospitalExitDateAsync(string userId,DateTime exitDate)
        {
            var existingPatientExitDate = await _patientExitDateQueryRepository.FindAsync(i => i.UserId == userId);

            if(existingPatientExitDate == null)
            {
                var patientExitDate = new PatientExitDate
                {
                    CreatedDate = DateTime.Now,
                    UserId = userId,
                    ExitDate = exitDate
                };

                await _patientExitDateCommandRepository.AddAsync(patientExitDate);
            }
            else
            {
                existingPatientExitDate!.ExitDate = exitDate;
                await _patientExitDateCommandRepository.UpdateAsync(existingPatientExitDate);
            }

            return true;
        }
    }
}

