using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.Models;
using BugWorkOrderSystem.Model.ViewModels.RoleProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugWorkOrderSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "CustomPolicy")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleServices services;

        public RoleController(IRoleServices services)
        {
            this.services = services;
        }

        [HttpGet("GetRoles")]
        public async Task<ActionResult<MessageModel<List<RoleViewDto>>>> GetRoles()
        {
            return await services.QueryList();
        }
    }
}
