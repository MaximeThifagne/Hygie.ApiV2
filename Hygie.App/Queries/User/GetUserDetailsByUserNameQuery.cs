using Hygie.Infrastructure.Common.Interfaces;
using Hygie.App.DTOs;
using MediatR;

namespace Hygie.App.Queries.User
{
    public class GetUserDetailsByUserNameQuery : IRequest<UserDetailsResponseDTO>
    {
        public required string UserName { get; set; }
    }

    public class GetUserDetailsByUserNameQueryHandler : IRequestHandler<GetUserDetailsByUserNameQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(request.UserName);
            return new UserDetailsResponseDTO() { Id = userId, FullName = fullName!, UserName = userName!, Email = email!, Roles = roles };
        }
    }
}

