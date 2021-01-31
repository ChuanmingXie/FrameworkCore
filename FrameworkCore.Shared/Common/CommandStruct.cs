/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common
*项目描述:
*类 名 称:CommadStruct
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:56:26
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.Common
{
    public class CommandStruct
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        public string CommandName { get; set; }

        /// <summary>
        /// 命令图类
        /// </summary>
        public string CommandKind { get; set; }

        /// <summary>
        /// 命令颜色
        /// </summary>
        public string CommandColor { get; set; }
    }
}
