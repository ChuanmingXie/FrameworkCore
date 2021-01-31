/*****************************************************************************
*项目名称:FrameworkCore.Service
*项目描述:
*类 名 称:Class1
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 0:58:19
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
    using FrameworkCore.Shared.Common.Collections;
    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.HttpContact;
    using RestSharp;
    using System.Threading.Tasks;

    /// <summary>
    /// CRUD请求基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseService<T>
    {
        private readonly string servicesName;

        public BaseService()
        {
            servicesName = typeof(T).Name.Replace("Dto", string.Empty);
        }

        /// <summary>
        /// 添加数据服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> AddAsync(T model)
        {
            var addService = await new BaseServiceRequest()
                .GetRequest<BaseResponse>($@"api/{servicesName}/Add", model, Method.POST);
            return addService;
        }

        /// <summary>
        /// 删除数据服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var deleteService = await new BaseServiceRequest()
                .GetRequest<BaseResponse>($@"api/{servicesName}/Delete?id={id}", string.Empty, Method.DELETE);
            return deleteService;
        }

        /// <summary>
        /// 改动数据服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> UpdateAsync(T model)
        {
            var updateService = await new BaseServiceRequest()
                .GetRequest<BaseResponse>($@"api/{servicesName}/Update", model, Method.POST);
            return updateService;
        }

        /// <summary>
        /// 查询列表数据服务
        /// </summary>
        /// <param name="pms"></param>
        /// <returns></returns>
        public async Task<BaseResponse<PagedList<T>>> GetAllListAsync(QueryParameters pms)
        {
            var getListServie = await new BaseServiceRequest()
                .GetRequest<BaseResponse<PagedList<T>>>($@"api/{servicesName}/GetAll?PageIndex={pms.PageIndex}&PageSize={pms.PageSize}&Search={pms.Search}", string.Empty, Method.GET);
            return getListServie;
        }

        /// <summary>
        /// 查询数据服务
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BaseResponse<T>> GetAsync(int id)
        {
            var getService = await new BaseServiceRequest()
                .GetRequest<BaseResponse<T>>($@"api/{servicesName}/Get?id={id}", string.Empty, Method.GET);
            return getService;
        }

        /// <summary>
        /// 保存数据服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<BaseResponse> SaveAsync(T model)
        {
            var saveService = await new BaseServiceRequest()
                .GetRequest<BaseResponse>($@"api/{servicesName}/Save", model, Method.POST);
            return saveService;
        }
    }
}
