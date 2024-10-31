using Common.Helpers;
using Inventory_Management.Common;
using Inventory_Management.Common.Exceptions;
using Inventory_Management.Common.Helpers;
using Inventory_Management.Entities;
using Inventory_Management.Features.Roles.AddRole.Commands;
using Inventory_Management.Features.Roles.GetRole.Queries;
using Inventory_Management.Features.Users.RegisterUser.Queries;
using MediatR;

namespace Inventory_Management.Features.Users.RegisterUser.Commands
{
  public record RegisterUserCommand(string UserName,string Password,string ConfirmPassword,string Email) : IRequest<ResultDto<int>>;

    public class RegisterUserCommandHandler : BaseRequestHandler<User, RegisterUserCommand, ResultDto<int>>
    {
        public RegisterUserCommandHandler(RequestParameters<User> requestParameters) : base(requestParameters)
        {
        }

        public async override Task<ResultDto<int>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new IsEmailExistQuery(request.Email));
            if (result.IsSuccess)
            {
                return ResultDto<int>.Faliure(ErrorCode.EmailAlreadyExist, "Email is already Exists");
            }
            result = await _mediator.Send(new IsUserNameExistQuery(request.UserName));
            if (result.IsSuccess)
            {
                return ResultDto<int>.Faliure(ErrorCode.UserNameAlreadyExist, "User Name is already Exists");
            }

            if (request.Password != request.ConfirmPassword)
            {
                return ResultDto<int>.Faliure(ErrorCode.PasswordsDontMatch, "Passwords don't match");
            }
            var user = request.MapOne<User>();
            user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            //Add Role
            var roleResult = await _mediator.Send(new GetRoleByNameQuery("Store Manager"));
            
            if (!roleResult.IsSuccess)
            {
                var newRoleResult = await _mediator.Send(new AddRoleCommand("Store Manager"));
                user.RoleID = newRoleResult.Data;
            }
            else
            {
                var role = roleResult.Data;
                user.RoleID = role.ID;
            }
             
           var newUser = await  _repository.AddAsync(user);
            await _repository.SaveChanges();
            return ResultDto<int>.Sucess(newUser.ID, "User Added Successfully");
        }
    }
}
