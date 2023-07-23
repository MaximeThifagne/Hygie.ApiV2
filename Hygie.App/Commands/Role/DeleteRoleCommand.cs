using Hygie.Infrastructure.Common.Interfaces;
using MediatR;

namespace Hygie.App.Commands.Role
{
    public class DeleteRoleCommand : IRequest<int>
    {
        public required string RoleId { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, int>
    {
        private readonly IIdentityService _identityService;

        public DeleteRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<int> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.DeleteRoleAsync(request.RoleId);
            return result ? 1 : 0;
        }
    }
}