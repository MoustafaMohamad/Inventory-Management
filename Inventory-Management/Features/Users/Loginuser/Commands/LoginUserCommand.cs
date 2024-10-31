using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using MediatR;

namespace Inventory_Management.Features.Users.Loginuser.Commands
{
    public record LoginUserCommand(string Email,string Password):IRequest<ResultDto<string>>;
    public class LoginUserCommandHandler : BaseRequestHandler<User, LoginUserCommand, ResultDto<string>>
    {
        public LoginUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
            
        }
        public async override Task<ResultDto<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _repository.FirstAsync(c => c.Email == request.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return ResultDto<string>.Faliure(ErrorCode.WrongPasswordOrEmail, "Email or Password is incorrect");
            }
            user.LastLogin = DateTime.UtcNow;
             _repository.Update(user);
            await _repository.SaveChanges();
            var token = await TokenGenerator.GenerateToken(user);
            if (!token.IsSuccess)
            {
                return ResultDto<string>.Faliure(token.ErrorCode, token.Message);
            }
            return ResultDto<string>.Sucess(token.Data, "User Login Successfully!");
        }
        }
}
