/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Enums
*项目描述:
*类 名 称:Notify
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:37:50
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FrameworkCore.Shared.Common.Enums
{
    public enum Notify
    {
        [Description("错误")]
        Error,

        [Description("警告")]
        Warning,

        [Description("提示信息")]
        Info,

        [Description("询问信息")]
        Question,
    }
}
