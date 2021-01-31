/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common
*项目描述:
*类 名 称:MsgInfo
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:57:14
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
    /// <summary>
    /// 信息提示结构
    /// </summary>
    public class MsgInfo
    {
        public bool IsOpen { get; set; }

        public string Msg { get; set; }
    }
}
