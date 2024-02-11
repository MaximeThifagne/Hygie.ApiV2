using System;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.User
{
    public class UpdateAdressCommand : IRequest<int>
    {
        public string Id { get; set; }
        public required string Number { get; set; }
        public required string Street { get; set; }
        public string? Complement { get; set; }
        public string? Neighborhood { get; set; }
        public required string City { get; set; }
        public required string ZipCode { get; set; }
    }

    public class UpdateAdressCommandHandler : IRequestHandler<UpdateAdressCommand, int>
    {
        private readonly IIdentityService _identityService;

        public UpdateAdressCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(UpdateAdressCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateAdress(request.Id, request.Number, request.Street, request.Complement, request.City, request.ZipCode);
            return result ? 1 : 0;
        }
    }
}

