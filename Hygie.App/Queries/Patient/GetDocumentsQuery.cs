using Hygie.Core.Entities;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Queries.Patient
{
    public class GetDocumentsQuery : IRequest<List<Document>>
    {
        public GetDocumentsQuery(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; private set; }
    }


    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, List<Document>>
    {
        private readonly IPatientService _patientService;

        public GetDocumentsQueryHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<List<Document>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
            => await _patientService.GetDocumentsAsync(request.UserId);
    }
}

