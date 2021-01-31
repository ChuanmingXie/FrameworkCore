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
    public class MenuController : Controller
    {
        private readonly IMenuManager menuManager;

        public MenuController(IMenuManager menuManager)
        {
            this.menuManager = menuManager;
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll([FromQuery] QueryParameters parameters) => await menuManager.GetAll(parameters);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="param">用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResponse> add([FromBody] MenuDto param) => await menuManager.Add(param);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ApiResponse> Delete(int id) => await menuManager.Delete(id);
    }
}
