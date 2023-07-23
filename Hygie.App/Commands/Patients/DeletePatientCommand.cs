using Hygie.Core.Repositories.Command;
using Hygie.Core.Repositories.Query;
using MediatR;

namespace Hygie.App.Commands.Patients
{
    // Patient create command with string response
    public class DeletePatientCommand : IRequest<String>
    {
        public Int64 Id { get; private set; }

        public DeletePatientCommand(Int64 Id)
        {
            this.Id = Id;
        }
    }

    // Patient delete command handler with string response as output
    public class DeletePatientCommmandHandler : IRequestHandler<DeletePatientCommand, String>
    {
        private readonly IPatientCommandRepository _patientCommandRepository;
        private readonly IPatientQueryRepository _patientQueryRepository;
        public DeletePatientCommmandHandler(IPatientCommandRepository patientRepository, IPatientQueryRepository patientQueryRepository)
        {
            _patientCommandRepository = patientRepository;
            _patientQueryRepository = patientQueryRepository;
        }

        public async Task<string> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var patientEntity = await _patientQueryRepository.GetByIdAsync(request.Id);

                await _patientCommandRepository.DeleteAsync(patientEntity);
            }
            catch (Exception exp)
            {
                throw (new ApplicationException(exp.Message));
            }

            return "Patient information has been deleted!";
        }
    }
}

