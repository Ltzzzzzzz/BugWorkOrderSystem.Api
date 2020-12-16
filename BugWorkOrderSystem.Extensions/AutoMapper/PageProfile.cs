using System;
using AutoMapper;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.PageProfiles;

namespace BugWorkOrderSystem.Extensions.AutoMapper
{
    public class PageProfile : Profile
    {
        public PageProfile()
        {
            CreateMap<Menu, MenuViewDto>();
        }
    }
}
