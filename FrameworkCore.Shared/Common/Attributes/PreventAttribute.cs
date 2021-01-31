/*****************************************************************************
*项目名称:FrameworkCore.Shared.Common.Attributes
*项目描述:
*类 名 称:PreventAttributes
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:42:29
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.Common.Attributes
{
    using System;


    /// <summary>
    /// 禁止序列化特征
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    class PreventAttribute : Attribute
    {
    }
}
