using AutoMapper;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using Inventory_Management.Features.Users.ChangePassword;
using Inventory_Management.Features.Users.ChangePassword.Commands;
using Inventory_Management.Features.Users.ForgetPassword;
using Inventory_Management.Features.Users.ForgetPassword.Orchestrators;
using Inventory_Management.Features.Users.Loginuser;
using Inventory_Management.Features.Users.Loginuser.Commands;
using Inventory_Management.Features.Users.RegisterUser;
using Inventory_Management.Features.Users.RegisterUser.Commands;
using Inventory_Management.Features.Users.ResetPassword;
using Inventory_Management.Features.Users.ResetPassword.Commands;

namespace Inventory_Management.Common.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ForgetPasswordEndPointRequest, ForgetPasswordOrchestrator>();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<ResetPasswordEndPointRequest, ResetPasswordCommand>();
            CreateMap<ChangePasswordEndPointRequest, ChangePasswordCommand>();
            CreateMap<RegisterUserEndPointRequest, RegisterUserCommand>();
            CreateMap<RegisterUserCommand, User>();
            CreateMap<LoginUserEndPointRequest, LoginUserCommand>();
        }
    }
}
