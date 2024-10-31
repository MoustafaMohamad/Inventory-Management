using Inventory_Management.Common;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Otps.RemoveOtp.Commands
{
    public record RemoveOtpCommand(OtpVerification otp) : IRequest<bool>;

    public class RemoveOtpCommandHandler : BaseRequestHandler<OtpVerification, RemoveOtpCommand, bool>
    {
        public RemoveOtpCommandHandler(RequestParameters<OtpVerification> requestParameters) : base(requestParameters)
        {
        }

        public override Task<bool> Handle(RemoveOtpCommand request, CancellationToken cancellationToken)
        {

            _repository.Delete(request.otp);

            //_repository.SaveChanges();
            return Task.FromResult(true);
        }
    }
}
