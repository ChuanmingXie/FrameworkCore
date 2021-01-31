/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:RestSharpRe
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:02:22
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.Service
{
    using FrameworkCore.Shared.Common.Aop;
    using FrameworkCore.Shared.HttpContact;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;

    public class RestSharpCertificateMethod
    {
        /// <summary>
        /// 请求数据
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求类型</param>
        /// <param name="pms">参数</param>
        /// <param name="isToken">是否Token</param>
        /// <param name="isJson">是否Json</param>
        /// <returns></returns>
        [GlobalLoger]
        public async Task<Response> RequestBehavior<Response>(string url, Method method
            , string pms, bool isToken = true, bool isJson = true) where Response : class
        {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(method);
            if (isToken)
                client.AddDefaultHeader("token", "");
            switch (method)
            {
                case Method.GET: request.AddHeader("Content-Type", "application/json"); break;
                case Method.POST:
                    if (isJson)
                    {
                        request.AddHeader("Content-Type", "application/json");
                        request.AddJsonBody(pms);
                    }
                    else
                    {
                        request.AddHeader("Content-Type", "application/json");
                        request.AddParameter("application/x-www-form-urlencoded", pms, ParameterType.RequestBody);
                    }
                    break;
                case Method.PUT: request.AddHeader("Content-Type", "application/json"); break;
                case Method.DELETE: request.AddHeader("Content-Type", "application/json"); break;
                default: request.AddHeader("Content-Type", "application/json"); break;
            }
            var response = await client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<Response>(response.Content);
            else
                return new BaseResponse()
                {
                    StatusCode = (int)response.StatusCode,
                    Message = response.StatusDescription ?? response.ErrorMessage
                } as Response;
        }
    }
}
