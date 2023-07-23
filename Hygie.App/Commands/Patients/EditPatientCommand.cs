using Hygie.App.DTOs;
using Hygie.App.Mapper;
using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command;
using Hygie.Core.Repositories.Query;
using MediatR;

namespace Hygie.App.Commands.Patients
{
    // Patient create command with PatientResponse
    public class EditPatientCommand : IRequest<PatientResponseDTO>
    {

        public Int64 Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ContactNumber { get; set; }
        public string? Address { get; set; }
    }

    public class EditPatientCommandHandler : IRequestHandler<EditPatientCommand, PatientResponseDTO>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;
        private readonly IPatientQueryRepository _patientQueryRepository;
        public EditPatientCommandHandler(IPatientCommandRepository patientRepository, IPatientQueryRepository patientQueryRepository)
        {
            _patientCommandRepository = patientRepository;
            _patientQueryRepository = patientQueryRepository;
        }
        public async Task<PatientResponseDTO> Handle(EditPatientCommand request, CancellationToken cancellationToken)
        {
            var patientEntity = MapperConfiguration.Mapper.Map<Patient>(request) ?? throw new ApplicationException("There is a problem in mapper");
            try
            {
                await _patientCommandRepository.UpdateAsync(patientEntity);
            }
            catch (Exception exp)
            {
                throw new ApplicationException(exp.Message);
            }

            var modifiedPatient = await _patientQueryRepository.GetByIdAsync(request.Id);
            var patientResponse = MapperConfiguration.Mapper.Map<PatientResponseDTO>(modifiedPatient);

            return patientResponse;
        }
    }
}
