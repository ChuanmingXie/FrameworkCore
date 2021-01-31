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
    public class GroupController : Controller
    {
        private readonly IGroupManager manager;

        /// <summary>
        /// 构造函数-分组参数注入
        /// </summary>
        /// <param name="manager"></param>
        public GroupController(IGroupManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 获取组列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters param) => await manager.GetAll(param);

        /// <summary>
        /// 保存组列表数据(新增/更新)
        /// </summary>
        /// <param name="model">组数据</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> Save([FromBody] GroupDataDto model) => await manager.Save(model);

        /// <summary>
        /// 删除组信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await manager.Delete(id);

        /// <summary>
        /// 获取菜单模块列表(包含每个菜单拥有的一些功能)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetMenuModules() => await manager.GetMenuModuleList();

        /// <summary>
        /// 查询组信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetGroupInfo(int id) => await manager.GetGroupData(id);
    }
}
