/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:AuthItem
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:01:29
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
    public class AuthItem: BaseEntity
    {
        /// <summary>
        /// 权限定义名称
        /// </summary>
        public string AuthName { get; set; }

        /// <summary>
        /// 设定预期图标
        /// </summary>
        public string AuthKind { get; set; }

        /// <summary>
        /// 设定预期颜色
        /// </summary>
        public string AuthColor { get; set; }

        /// <summary>
        /// 所属权限值
        /// </summary>
        public int AuthValue { get; set; }
    }
}
