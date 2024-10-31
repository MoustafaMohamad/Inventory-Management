using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Otps.StoreOtp.Queries
{
    public record GetOtpQuery(string email, string otp) : IRequest<ResultDto<OtpVerification>>;

    public class GetOtpQueryHandler : BaseRequestHandler<OtpVerification, GetOtpQuery, ResultDto<OtpVerification>>
    {
        public GetOtpQueryHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<OtpVerification>> Handle(GetOtpQuery request, CancellationToken cancellationToken)
        {
            var otp = await _repository.FirstAsync(ot => ot.Email == request.email && ot.Otp == request.otp);
            if (otp is null)
            {
                return ResultDto<OtpVerification>.Faliure(ErrorCode.OtpIsNotFound, "Otp is not Found");
            }
            return ResultDto<OtpVerification>.Sucess(otp);
        }


    }
}
