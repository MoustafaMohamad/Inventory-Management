using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Otps.RemoveOtp.Commands;
using Inventory_Management.Features.Otps.StoreOtp.Queries;
using MediatR;

namespace Inventory_Management.Features.Users.ResetPassword.Queries
{
    public record ValidateOtpQuery(string email, string otp) : IRequest<ResultDto<bool>>;

    public class ValidateOtpQueryHandler : BaseRequestHandler<OtpVerification, ValidateOtpQuery, ResultDto<bool>>
    {
        public ValidateOtpQueryHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }
        public async override Task<ResultDto<bool>> Handle(ValidateOtpQuery request, CancellationToken cancellationToken)
        {
            var otpValidationResult = await _mediator.Send(new GetOtpQuery(request.email, request.otp));
            if (!otpValidationResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(otpValidationResult.ErrorCode, otpValidationResult.Message);
            }
            var otp = otpValidationResult.Data;
            if (otp == null || otp.ExpiryTime < DateTime.UtcNow)
            {
                return ResultDto<bool>.Faliure(ErrorCode.OtpIsNotValid, "Otp is not Valid");
            }
           //await _mediator.Send(new RemoveOtpCommand(otp));

            await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true);
        }


    }

}
