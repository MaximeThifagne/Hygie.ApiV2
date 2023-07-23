using Hygie.App.DTOs;
using Hygie.Infrastructure.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hygie.App.Queries.User
{
    public class GetAllUsersByRoleQuery : IRequest<List<UserDetailsResponseDTO>>
    {
        public required string RoleName { get; set; }
    }

    public class GetAllUsersByRoleQueryHandler : IRequestHandler<GetAllUsersByRoleQuery, List<UserDetailsResponseDTO>>
    {
        private readonly IIdentityService _identityService;

        public GetAllUsersByRoleQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserDetailsResponseDTO>> Handle(GetAllUsersByRoleQuery request, CancellationToken cancellationToken)
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

            return userDetails.Where(u => u.Roles.Contains(request.RoleName)).ToList();
        }
    }
}
