/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:BasicType
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:03:41
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
    public class BasicType : BaseEntity
    {
        /// <summary>
        /// 字典代码
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string TypeName { get; set; }
    }
}
