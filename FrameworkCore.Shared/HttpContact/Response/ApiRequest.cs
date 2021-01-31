/*****************************************************************************
*项目名称:FrameworkCore.Shared.HttpContact.Response
*项目描述:
*类 名 称:ApiRequest
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 23:23:10
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Shared.HttpContact.Response
{
    /// <summary>
    /// 请求返回定义类
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// 设置状态与收集信息
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        public ApiResponse(int statusCode, string message = "")
        {
            this.StatusCode = statusCode;
            this.Message = message;
        }

        /// <summary>
        /// 设置状态与收集结果
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="result"></param>
        public ApiResponse(int statusCode, object result = null)
        {
            this.StatusCode = statusCode;
            this.Result = result;
        }

        /// <summary>
        /// 后台消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// //返回状态
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public object Result { get; set; }
    }
}
