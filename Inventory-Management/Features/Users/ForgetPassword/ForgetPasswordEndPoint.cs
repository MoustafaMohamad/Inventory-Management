
using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.ForgetPassword.Orchestrators;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.ForgetPassword
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ForgetPasswordEndPoint :ControllerBase
    {
        private readonly IMediator _mediator;
        public ForgetPasswordEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Forget-password")]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgetPasswordEndPointRequest request)
        {
            var result =await  _mediator.Send(request.MapOne<ForgetPasswordOrchestrator>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode,result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data));
        }
    }
}
