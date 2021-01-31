/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:UserConfig
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:08:08
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
    public class UserConfig : BaseEntity
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 预计支出
        /// </summary>
        public decimal ExpectedOut { get; set; }

        /// <summary>
        /// 预计收入
        /// </summary>
        public decimal ExpectedIn { get; set; }
    }
}
