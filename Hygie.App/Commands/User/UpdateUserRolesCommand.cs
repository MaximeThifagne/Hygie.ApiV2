using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.User
{
    public class UpdateUserRolesCommand : IRequest<int>
    {
        public required string UserName { get; set; }
        public required IList<string> Roles { get; set; }
    }

    public class UpdateUserRolesCommandHandler : IRequestHandler<UpdateUserRolesCommand, int>
    {
        private readonly IIdentityService _identityService;

        public UpdateUserRolesCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.UpdateUsersRole(request.UserName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
