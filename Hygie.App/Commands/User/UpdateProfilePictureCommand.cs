using Hygie.Infrastructure.Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hygie.App.Commands.User
{
    public class UpdateProfilePictureCommand : IRequest<int>
    {
        public string Id { get; set; }
        public IFormFile Data { get; set; }
    }

    public class UpdateProfilePictureCommandHandler : IRequestHandler<UpdateProfilePictureCommand, int>
    {
        private readonly IIdentityService _identityService;

        public UpdateProfilePictureCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<int> Handle(UpdateProfilePictureCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateProfilePictureCommand(request.Id, request.Data);
            return result ? 1 : 0;
        }
    }
}
