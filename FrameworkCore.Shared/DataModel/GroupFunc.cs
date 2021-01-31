/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:GroupFun
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:04:45
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
    /// <summary>
    /// 组功能
    /// </summary>
    public class GroupFunc : BaseEntity
    {
        /// <summary>
        /// 组代码
        /// </summary>
        public string GroupCode { get; set; }

        /// <summary>
        /// 菜单代码
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int Auth { get; set; }
    }
}
