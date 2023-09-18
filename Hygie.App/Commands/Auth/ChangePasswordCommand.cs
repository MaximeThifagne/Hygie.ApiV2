using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.Auth
{
    public class ChangePasswordCommand : IRequest<int>
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, int>
    {
        private readonly IIdentityService _identityService;

        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<int> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangePassword(request.Id, request.Token, request.Password, request.ConfirmPassword);

            return result ? 1 : 0;
        }
    }
}
