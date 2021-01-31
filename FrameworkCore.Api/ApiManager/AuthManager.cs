using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.ApiManager
{
    using FrameworkCore.EFCore;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.HttpContact.Response;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AuthManager : IAuthItemManager
    {
        private readonly ILogger<AuthManager> logger;
        private readonly IUnitOfWork work;

        /// <summary>
        /// 构造函数-注入参数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="work"></param>
        public AuthManager(ILogger<AuthManager> logger, IUnitOfWork work)
        {
            this.logger = logger;
            this.work = work;
        }

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GeAll()
        {
            try
            {
                var models = await work.GetRepository<AuthItem>().GetAllAsync();
                return new ApiResponse(200, models.OrderBy(t => t.AuthValue).ToList());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return new ApiResponse(201, "");
            }
        }
    }
}
