using System.Collections.Generic;
using System.Threading.Tasks;
using BugWorkOrderSystem.IServices;
using BugWorkOrderSystem.Model;
using BugWorkOrderSystem.Model.ViewModels.PageProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BugWorkOrderSystem.Api.Controllers
{
    /// <summary>
    /// 页面控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize("CustomPolicy")]
    public class PageController : ControllerBase
    {
        private readonly IMenuServices menuServices;

        public PageController(IMenuServices menuServices)
        {
            this.menuServices = menuServices;
        }
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenu")]
        public async Task<ActionResult<MessageModel<List<MenuViewDto>>>> GetMenu()
        {
            var menus = await menuServices.GetMenus();
            return new MessageModel<List<MenuViewDto>>
            {
                Success = true,
                Message = "菜单获取成功",
                Data = menus
            };
        }
    }
}
