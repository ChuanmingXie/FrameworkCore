/*****************************************************************************
*项目名称:FrameworkCore.Shared.HttpContact
*项目描述:
*类 名 称:BaseResponse
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:15:21
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.HttpContact
{
    public class BaseResponse
    {
        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返沪状态
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 结果
        /// </summary>
        public object Result { get; set; }
    }

    public class BaseResponse<T>
    {
        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回状态
        /// </summary>
        public int SatusCode { get; set; }

        public T Result { get; set; }
    }
}
