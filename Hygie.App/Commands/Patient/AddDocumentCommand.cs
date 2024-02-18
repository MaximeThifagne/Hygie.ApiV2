using System;
using Hygie.App.Commands.User;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hygie.App.Commands.Patient
{
    public class AddDocumentCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public IFormFile File { get; set; }
        public string Type { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }

    public class AddDocumentCommandHandler : IRequestHandler<AddDocumentCommand, bool>
    {
        private readonly IPatientService _patientService;

        public AddDocumentCommandHandler(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public async Task<bool> Handle(AddDocumentCommand request, CancellationToken cancellationToken)
        {
            var result = await _patientService.AddDocument(request.UserId,request.File, request.Type, request.ExpirationDate);
            return result ? true : false;
        }
    }
}

