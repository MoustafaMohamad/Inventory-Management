using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Helpers;
using Inventory_Management.CQRS.Email.Command;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Queries;
using Inventory_Management.Features.Otps.StoreOtp.Commands;
using MediatR;

namespace Inventory_Management.Features.Users.ForgetPassword.Orchestrators
{
    public record ForgetPasswordOrchestrator(string Email) : IRequest<ResultDto<bool>>;
    public class ForgetPasswordOrchestratorhandler : BaseRequestHandler<User, ForgetPasswordOrchestrator, ResultDto<bool>>
    {

        public ForgetPasswordOrchestratorhandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }
        public async override Task<ResultDto<bool>> Handle(ForgetPasswordOrchestrator request, CancellationToken cancellationToken)
        {
            var userResult = await _mediator.Send(new GetUserByEmailQuery(request.Email));
            if (!userResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(userResult.ErrorCode, userResult.Message);
            }
            var user = userResult.Data.MapOne<User>();
            var otpCode = OTPGenerator.GenerateOTP();
            //user.OtpCode = otpCode;
            var OtpStoreResult = _mediator.Send(new StoreOtpCommand(request.Email, otpCode));
            var emailResult = _mediator.Send(new SendEmailCommand(user.Email, "OTP", otpCode));
            return ResultDto<bool>.Sucess(true, "OTP Had been sent to your Email");
        }
    }
}
