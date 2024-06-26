﻿using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Exceptions;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.Auth
{
    public class AuthCommand : IRequest<AuthResponseDTO>
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;

        public AuthCommandHandler(IIdentityService identityService, ITokenGenerator tokenGenerator)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);

            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var(userId, fullName, userName, email,emailVerified,phoneNumber, roles,adress, profileImage) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            string token = _tokenGenerator.GenerateJWTToken((userId, userName!, roles));

            return new AuthResponseDTO()
            {
                UserId = userId,
                Name = userName!,
                FullName = fullName!,
                EmailVerified = emailVerified ?? false,
                Email = email!,
                Token = token,
                PhoneNumber = phoneNumber,
                Roles = roles,
                Adress = adress,
                ProfileImage = profileImage
            };
        }
    }
}
