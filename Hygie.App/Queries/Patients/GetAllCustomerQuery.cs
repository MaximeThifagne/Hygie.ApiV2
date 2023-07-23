using Hygie.Core.Entities;
using Hygie.Core.Repositories.Query;
using MediatR;

namespace Hygie.App.Queries.Patients
{
    // Patient query with List<Patient> response
    public record GetAllPatientQuery : IRequest<List<Patient>>
    {

    }

    public class GetAllPatientHandler : IRequestHandler<GetAllPatientQuery, List<Patient>>
    {
        private readonly IPatientQueryRepository _patientQueryRepository;

        public GetAllPatientHandler(IPatientQueryRepository patientQueryRepository)
        {
            _patientQueryRepository = patientQueryRepository;
        }
        public async Task<List<Patient>> Handle(GetAllPatientQuery request, CancellationToken cancellationToken)
        {
            return (List<Patient>)await _patientQueryRepository.GetAllAsync();
        }
    }
}
