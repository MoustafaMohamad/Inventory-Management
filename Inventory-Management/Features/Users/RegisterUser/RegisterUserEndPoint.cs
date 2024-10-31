using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers.ResultViewModel;
using Inventory_Management.Features.Users.RegisterUser.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management.Features.Users.RegisterUser
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class RegisterUserEndPoint : ControllerBase
    {
        private readonly IMediator _mediator;

        public RegisterUserEndPoint(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register-user")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserEndPointRequest request)
        {
            var result = await _mediator.Send(request.MapOne<RegisterUserCommand>());
            if (!result.IsSuccess)
            {
                throw new BusinessException(result.ErrorCode, result.Message);
            }
            return Ok(ResultViewModel.Sucess(result.Data, result.Message));
        }
    }
}
