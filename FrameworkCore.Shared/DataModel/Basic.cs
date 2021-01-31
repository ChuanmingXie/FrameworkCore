/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataModel
*项目描述:
*类 名 称:Basic
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:03:11
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
    public class Basic : BaseEntity
    {
        /// <summary>
        /// 字典类型代码
        /// </summary>
        public string TypeCode { get; set; }

        /// <summary>
        /// 数据编码
        /// </summary>
        public string DataCode { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string NativeName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        public string EnglishName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdate { get; set; }

        /// <summary>
        /// 最后更新人
        /// </summary>
        public string LastUpdateBy { get; set; }
    }
}
