using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using MediatR;
using Common.Helpers;

namespace Inventory_Management.Features.Roles.GetRole.Queries
{
  public record GetRoleByNameQuery(string Name) : IRequest<ResultDto<Role>>;
    public class GetRoleByNameQueryHandler : BaseRequestHandler<Role, GetRoleByNameQuery, ResultDto<Role>>
    {
        public GetRoleByNameQueryHandler(RequestParameters<Role> requestParameters) : base(requestParameters)
        {

        }

        public async override Task<ResultDto<Role>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.FirstAsync(u => u.Name == request.Name);
            if (role is null)
            {
                return ResultDto<Role>.Faliure(ErrorCode.RoleNotFound, "Role is not Found");
            }
            return ResultDto<Role>.Sucess(role);
        }
    }
}
