using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.Loginuser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.Loginuser
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class LoginUserEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public LoginUserEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Login-user")]
        public async Task<ActionResult> LoginUserAsync([FromBody] LoginUserEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<LoginUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }
    }
}
