using System;
using AutoMapper;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;

namespace BugWorkOrderSystem.Extensions.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewDto>();
        }
    }
}
