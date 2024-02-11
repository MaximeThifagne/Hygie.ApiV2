using System;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.User
{
    public class UpdatePhoneNumberCommand : IRequest<int>
    {
        public required string Id { get; set; }

        public required string PhoneNumber { get; set; }
    }

    public class UpdatePhoneNumberCommandHandler : IRequestHandler<UpdatePhoneNumberCommand, int>
    {
        private readonly IIdentityService _identityService;

        public UpdatePhoneNumberCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdatePhoneNumber(request.Id, request.PhoneNumber);
            return result ? 1 : 0;
        }
    }
}

