using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Exceptions;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygie.App.Commands.Auth
{
    public class ResetPasswordCommand : IRequest<int>
    {
        public required string Email { get; set; }
    }

    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, int>
    {
        private readonly IIdentityService _identityService;
        private readonly IMailService _emailService;

        public ResetPasswordCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, IMailService emailService)
        {
            _identityService = identityService;
            _emailService = emailService;
        }

        public async Task<int> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendResetPasswordLink(request.Email);

            return 1;
        }
    }
}
