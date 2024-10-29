﻿using AutoMapper;
using Inventory_Management.Entities;
using Inventory_Management.Features.Common.Users.Dto;
using Inventory_Management.Features.Users.ChangePassword;
using Inventory_Management.Features.Users.ChangePassword.Commands;
using Inventory_Management.Features.Users.ForgetPassword;
using Inventory_Management.Features.Users.ForgetPassword.Commands;
using Inventory_Management.Features.Users.ResetPassword;
using Inventory_Management.Features.Users.ResetPassword.Commands;

namespace Inventory_Management.Common.Profiles
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<ForgetPasswordEndPointRequest, ForgetPasswordCommand>();
            CreateMap<UserDto,User>().ReverseMap();
            CreateMap<ResetPasswordEndPointRequest, ResetPasswordCommand>();
            CreateMap<ChangePasswordEndPointRequest, ChangePasswordCommand>();
        }
    }
}
