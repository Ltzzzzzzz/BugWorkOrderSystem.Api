using System.ComponentModel.DataAnnotations;

namespace BugWorkOrderSystem.Model.ViewModels.Roles
{
    public class RoleAdd : RootEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        [Required(ErrorMessage = "请输入角色名称")]
        [StringLength(10, ErrorMessage = "角色名称最大长度是10")]
        public string Name { get; set; }
    }
}
