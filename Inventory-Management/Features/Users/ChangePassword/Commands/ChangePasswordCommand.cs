using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Users.ChangePassword.Commands
{

    public record ChangePasswordCommand(string Email, string oldPassword, string newPassword, string ConfirmPassword) : IRequest<ResultDto<bool>>;
    public class ChangePasswordCommandhandler : BaseRequestHandler<User, ChangePasswordCommand, ResultDto<bool>>
    {

        public ChangePasswordCommandhandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {

        }
        public async override Task<ResultDto<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstAsync(c => c.Email == request.Email);
            if (user is null)
            {
                return ResultDto<bool>.Faliure(ErrorCode.EmailIsNotFound, "Email is not Found");
            }
            var oldPasswordCheckResult = BCrypt.Net.BCrypt.Verify(request.oldPassword, user.Password);
            if (!oldPasswordCheckResult)
            {
                return ResultDto<bool>.Faliure(ErrorCode.WrongPassword, "Invalid Password");
            }
            if (request.newPassword != request.ConfirmPassword)
            {
                return ResultDto<bool>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords Don't Match");
            }
            //validate new password
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.newPassword);
             _repository.Update(user);
            await _repository.SaveChanges();
            return ResultDto<bool>.Sucess(true, "Password had been Changed");
        }
    }
}
