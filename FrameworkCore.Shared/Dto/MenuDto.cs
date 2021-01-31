/*****************************************************************************
*项目名称:FrameworkCore.Shared.Dto
*项目描述:
*类 名 称:MenuDto
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:28:18
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Dto
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    public class MenuDto : BaseDto
    {
        /// <summary>
        /// 菜单代码
        /// </summary>
        [Description("菜单代码")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Description("菜单名称")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        [Description("菜单标题")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string MenuCaption { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string MenuNameSpace { get; set; }


        /// <summary>
        /// 所属权限值
        /// </summary>
        public int MenuAuth { get; set; }
    }
}
