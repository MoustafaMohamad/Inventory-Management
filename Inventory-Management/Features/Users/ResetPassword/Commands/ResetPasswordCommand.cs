using Inventory_Management.Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Queries;
using MediatR;
using Common.Helpers;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Features.Users.ResetPassword.Queries;


namespace Inventory_Management.Features.Users.ResetPassword.Commands
{
    public record ResetPasswordCommand(string Email,string Otp,string NewPassword,string ConfirmPassword) : IRequest<ResultDto<bool>>;
    public class ResetPasswordCommandhandler : BaseRequestHandler<User, ResetPasswordCommand, ResultDto<bool>>
    {

        public ResetPasswordCommandhandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }
        public async override Task<ResultDto<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            
            var user = await _repository.FirstAsync(c => c.Email == request.Email);
            if (user is null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is not Found");
            }
            var otpValidationResult = await _mediator.Send(new ValidateOtpQuery(request.Email,request.Otp));
            if (!otpValidationResult.IsSuccess)
            {
                return ResultDto<bool>.Faliure(otpValidationResult.ErrorCode, otpValidationResult.Message);
            }
            if (request.NewPassword!=request.ConfirmPassword)
            {
                return ResultDto<bool>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords Don't Match");
            }
            //validate new password
           user.Password= BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            _repository.Update(user);
            await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true, "Password had been Changed");
        }
    }
}
