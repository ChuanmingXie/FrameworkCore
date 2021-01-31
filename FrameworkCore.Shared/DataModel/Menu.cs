/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:Menu
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:05:53
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.DataModel
{
    using System.ComponentModel;

    public class Menu : BaseEntity
    {
        /// <summary>
        /// 菜单代码
        /// </summary>
        [Description("菜单代码")]
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Description("菜单名称")]
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单标题
        /// </summary>
        [Description("菜单标题")]
        public string MenuCaption { get; set; }

        /// <summary>
        /// 命名空间
        /// </summary>
        public string MenuNameSpcace { get; set; }

        /// <summary>
        /// 所属权限值
        /// </summary>
        public int MenuAuth { get; set; }
    }
}
