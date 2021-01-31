/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:Group
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:04:07
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FrameworkCore.Shared.DataModel
{
    public class Group : BaseEntity
    {
        /// <summary>
        /// 组代码
        /// </summary>
        [Description("组代码")]
        public string GroupCode { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        [Description("组名称")]
        public string GroupName { get; set; }
    }
}
