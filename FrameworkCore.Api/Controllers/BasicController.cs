using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.Controllers
{
    using FrameworkCore.Api.ApiManager;
    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact.Response;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BasicController : ControllerBase
    {
        private readonly IBasicManager manager;

        public BasicController(IBasicManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 获取基础数据列表
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters param) => await manager.GetAll(param);

        /// <summary>
        /// 新增基础数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Add([FromBody] BasicDto param) => await manager.Add(param);

        /// <summary>
        /// 更新基础数据
        /// </summary>
        /// <param name="param">请求参数</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Update([FromBody] BasicDto param) => await manager.Save(param);

        /// <summary>
        /// 删除集成数据
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await manager.Delete(id);
    }
}
