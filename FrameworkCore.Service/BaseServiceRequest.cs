/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:BaseServiceRequest
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:00:56
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Service
{
    using FrameworkCore.Shared.Common;
    using FrameworkCore.Shared.HttpContact;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Threading.Tasks;

    /// <summary>
    /// 请求服务基类
    /// </summary>
    public class BaseServiceRequest
    {
        private readonly string requestUrl = Contract.serverUrl;

        public string RequestUrl
        {
            get { return requestUrl; }
        }

        /// <summary>
        /// restSharp实例
        /// </summary>
        public RestSharpCertificateMethod restSharp = new RestSharpCertificateMethod();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <param name="request"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<Response> GetRequest<Response>(BaseRequest request, Method method) where Response : class
        {
            string pms = request.GetPropertiesObject();
            string url = requestUrl + request.Route;
            if (!string.IsNullOrWhiteSpace(request.getParameter))
                url += request.getParameter;
            Response result = await restSharp.RequestBehavior<Response>(url, method, pms);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Response"></typeparam>
        /// <param name="route"></param>
        /// <param name="obj"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public async Task<Response> GetRequest<Response>(string route, object obj, Method method) where Response : class
        {
            string pms = string.Empty;
            if (!string.IsNullOrWhiteSpace(obj?.ToString()))
                pms = JsonConvert.SerializeObject(obj);
            Response result = await restSharp.RequestBehavior<Response>(requestUrl + route, method, pms);
            return result;
        }
    }
}
