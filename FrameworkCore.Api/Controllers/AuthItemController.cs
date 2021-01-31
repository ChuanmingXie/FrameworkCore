using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.Controllers
{
    using System.Threading.Tasks;
    using FrameworkCore.Api.ApiManager;
    using FrameworkCore.Shared.HttpContact.Response;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthItemController : Controller
    {
        private readonly IAuthItemManager manager;

        public AuthItemController(IAuthItemManager manager)
        {
            this.manager = manager;
        }

        /// <summary>
        /// 获取所有功能按钮列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResponse> GetAll() => await manager.GeAll();
    }
}
