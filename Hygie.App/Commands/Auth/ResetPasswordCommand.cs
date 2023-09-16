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
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        private readonly IMailService _emailService;

        public ResetPasswordCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator, IMailService emailService)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
            _emailService = emailService;
        }

        public async Task<int> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _identityService.IsExistByEmail(request.Email);

            if (!isExist)
            {
                throw new BadRequestException("Utilisateur inconnu");
            }

            await _emailService.SendResetPasswordLink(request.Email);

            return 1;
        }
    }
}
