using Hygie.App.DTOs;
using Hygie.App.Mapper;
using Hygie.Core.Entities;
using Hygie.Core.Repositories.Command;
using MediatR;

namespace Hygie.App.Commands.Patients
{
    // Patient create command with PatientResponse
    public class CreatePatientCommand : IRequest<PatientResponseDTO>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string ContactNumber { get; set; }
        public required string Address { get; set; }
        public DateTime CreatedDate { get; set; }

        public CreatePatientCommand()
        {
            this.CreatedDate = DateTime.Now;
        }
    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, PatientResponseDTO>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;
        public CreatePatientCommandHandler(IPatientCommandRepository patientCommandRepository)
        {
            _patientCommandRepository = patientCommandRepository;
        }
        public async Task<PatientResponseDTO> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var patientEntity = MapperConfiguration.Mapper.Map<Patient>(request) ?? throw new ApplicationException("There is a problem in mapper");
            var newPatient = await _patientCommandRepository.AddAsync(patientEntity);
            var patientResponse = MapperConfiguration.Mapper.Map<PatientResponseDTO>(newPatient);
            return patientResponse;
        }
    }
}
