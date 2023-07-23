using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Queries.User
{
    public class GetUserDetailsQuery : IRequest<UserDetailsResponseDTO>
    {
        public required string UserId { get; set; }
    }

    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetUserDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(request.UserId);
            return new UserDetailsResponseDTO() { Id = userId, FullName = fullName!, UserName = userName!, Email = email!, Roles = roles };
        }
    }
}
