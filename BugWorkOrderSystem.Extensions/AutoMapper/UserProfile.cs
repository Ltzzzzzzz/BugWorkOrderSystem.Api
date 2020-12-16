using System;
using AutoMapper;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.UserProfiles;

namespace BugWorkOrderSystem.Extensions.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserLoginDto, User>();

            CreateMap<User, UserViewDto>();

            CreateMap<UserUpdateDto, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(s => s.NewPassword));

            CreateMap<User, UsersViewDto>();

            CreateMap<User, SysUserViewDto>();

            CreateMap<UserUpdateDto, UserViewDto>();
        }
    }
}
