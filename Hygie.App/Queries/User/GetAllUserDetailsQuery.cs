using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Queries.User
{
    public class GetAllUsersDetailsQuery : IRequest<List<UserDetailsResponseDTO>>
    {
    }

    public class GetAllUsersDetailsQueryHandler : IRequestHandler<GetAllUsersDetailsQuery, List<UserDetailsResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetAllUsersDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserDetailsResponseDTO>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {


            var users = await _identityService.GetAllUsersAsync();
            var userDetails = users.Select(x => new UserDetailsResponseDTO()
            {
                Id = x.id,
                Email = x.email,
                UserName = x.userName
            }).ToList();

            foreach (var user in userDetails)
            {
                user.Roles = await _identityService.GetUserRolesAsync(user.Id);
            }
            return userDetails;
        }
    }
}
