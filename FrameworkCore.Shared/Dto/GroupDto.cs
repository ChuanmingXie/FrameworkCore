/*****************************************************************************
*项目名称:FrameworkCore.Shared.Dto
*项目描述:
*类 名 称:GroupDto
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:26:19
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Dto
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class GroupDto : BaseDto
    {
        [Description("组代码")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} charaters long.", MinimumLength = 2)]
        public string GroupCode { get; set; }

        [Description("组名称")]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} charaters long.", MinimumLength = 2)]
        public string GroupName { get; set; }
    }
}
