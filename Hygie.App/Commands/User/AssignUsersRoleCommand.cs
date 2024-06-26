﻿using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.User
{
    public class AssignUsersRoleCommand : IRequest<int>
    {
        public required string UserName { get; set; }
        public required IList<string> Roles { get; set; }
    }

    public class AssignUsersRoleCommandHandler : IRequestHandler<AssignUsersRoleCommand, int>
    {
        private readonly IIdentityService _identityService;

        public AssignUsersRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(AssignUsersRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.AssignUserToRole(request.UserName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
