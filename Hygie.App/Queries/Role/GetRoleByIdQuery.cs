using Hygie.Infrastructure.Common.Interfaces;
using Hygie.App.DTOs;
using MediatR;

namespace Hygie.App.Queries.Role
{
    public class GetRoleByIdQuery : IRequest<RoleResponseDTO>
    {
        public required string RoleId { get; set; }
    }

    public class GetRoleQueryByIdHandler : IRequestHandler<GetRoleByIdQuery, RoleResponseDTO>
    {
        private readonly IIdentityService _identityService;

        public GetRoleQueryByIdHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<RoleResponseDTO> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var (id, roleName) = await _identityService.GetRoleByIdAsync(request.RoleId);
            return new RoleResponseDTO() { Id = id, RoleName = roleName };
        }
    }
}
