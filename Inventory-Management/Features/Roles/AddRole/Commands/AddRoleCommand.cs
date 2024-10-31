using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Roles.Common.DTO;
using MediatR;

namespace Inventory_Management.Features.Roles.AddRole.Commands
{
    public record AddRoleCommand (string Name):IRequest<ResultDto<int>>;

    public class AddRoleCommandHandeler : BaseRequestHandler<Role, AddRoleCommand, ResultDto<int>>
    {
        public AddRoleCommandHandeler(RequestParameters<Role> requestParameters) : base(requestParameters)
        {
        }

        public override async Task<ResultDto<int>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.AddAsync(new Role
            {
                Name = request.Name
            });
            await _repository.SaveChanges();
            return ResultDto<int>.Sucess(role.ID,"Role Added Successfully");
        }
    }
}
