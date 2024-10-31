using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Roles.AddRole.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Roles.AddRole
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AddRoleEndPoint : ControllerBase 
    {
        private readonly IMediator _mediator;

        public AddRoleEndPoint(IMediator mediator )     
        {
            _mediator = mediator;
        }


        [HttpPost("Add-role")]
        public async Task<ResultViewModel> AddRoleAsync(AddRoleEndPointRequest request)
        {
          var result=   await _mediator.Send(new AddRoleCommand(request.RoleName));

            return ResultViewModel.Sucess(result);
            
        }

    }
}
