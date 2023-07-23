using Hygie.Core.Entities;
using Hygie.Infrastructure.Common.Exceptions;
using MediatR;

namespace Hygie.App.Queries.Patients
{
    // Patient GetPatientByIdQuery with Patient response
    public class GetPatientByIdQuery : IRequest<Patient>
    {
        public Int64 Id { get; private set; }

        public GetPatientByIdQuery(Int64 Id)
        {
            this.Id = Id;
        }

    }

    public class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, Patient>
    {
        private readonly IMediator _mediator;

        public GetPatientByIdHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<Patient> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patients = await _mediator.Send(new GetAllPatientQuery(), cancellationToken);
            var selectedPatient = patients.FirstOrDefault(x => x.Id == request.Id);
            if (selectedPatient != null)
                return selectedPatient;
            else
                throw new NotFoundException(nameof(Patient), request.Id);
        }
    }
}
