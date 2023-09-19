using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.User
{
    public class CreateUserCommand : IRequest<int>
    {
        public required string FullName { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmationPassword { get; set; }
        public required string Role { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IIdentityService _identityService;
        private readonly IMailService _mailService;
        public CreateUserCommandHandler(IIdentityService identityService, IMailService mailService)
        {
            _identityService = identityService;
            _mailService = mailService;
        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var (isSucceed, userId) = await _identityService.CreateUserAsync(request.UserName, request.Password, request.Email, request.FullName, request.Role);
            if (isSucceed)
            {
                await _mailService.SendConfirmEmailLink(request.Email);
            }

            return isSucceed ? 1 : 0;
        }
    }
}