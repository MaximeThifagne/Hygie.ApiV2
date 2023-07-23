using Hygie.Core.Entities;
using Hygie.Infrastructure.Common.Exceptions;
using MediatR;

namespace Hygie.App.Queries.Patients
{
    // Patient GetPatientByEmailQuery with Patient response
    public class GetPatientByEmailQuery : IRequest<Patient>
    {
        public string Email { get; private set; }

        public GetPatientByEmailQuery(string email)
        {
            this.Email = email;
        }

    }

    public class GetPatientByEmailHandler : IRequestHandler<GetPatientByEmailQuery, Patient>
    {
        private readonly IMediator _mediator;

        public GetPatientByEmailHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Patient> Handle(GetPatientByEmailQuery request, CancellationToken cancellationToken)
        {
            var patients = await _mediator.Send(new GetAllPatientQuery(), cancellationToken);
            var selectedPatient = patients.FirstOrDefault(x => x.Email.ToLower().Contains(request.Email.ToLower()));
            if (selectedPatient != null)
                return selectedPatient;
            else
                throw new NotFoundException(nameof(Patient), request.Email);
        }
    }
}
