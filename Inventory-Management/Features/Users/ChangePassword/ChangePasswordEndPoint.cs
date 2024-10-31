using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ChangePassword.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ChangePassword
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ChangePasswordEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;
        public ChangePasswordEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<ChangePasswordCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}
