using System;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.Patient
{
    public class SetHospitalExitDateCommand : IRequest<int>
    {
            public string? UserId { get; set; }
            public DateTime ExitDate { get; set; }
    }

    public class SetHospitalExitDateCommandHandler: IRequestHandler<SetHospitalExitDateCommand, int>
    {
        private readonly IPatientService _patientService;

        public SetHospitalExitDateCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<int> Handle(SetHospitalExitDateCommand request, CancellationToken cancellationToken)
        {
            var result = await _patientService.SetHospitalExitDateAsync(request.UserId,request.ExitDate);

            return result ? 1 : 0;
        }
    }
}

