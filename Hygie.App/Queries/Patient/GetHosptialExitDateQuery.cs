using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Queries.Patient
{
    public class GetHospitilExitDateQuery : IRequest<DateTime?>
    {
        public GetHospitilExitDateQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }


    public class GetHospitalExitDateQueryHandler : IRequestHandler<GetHospitilExitDateQuery, DateTime?>
    {
        private readonly IPatientService _patientService;

        public GetHospitalExitDateQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<DateTime?> Handle(GetHospitilExitDateQuery request, CancellationToken cancellationToken)
            => await _patientService.GetHospitalExitDateAsync(request.UserId);
    }
}

