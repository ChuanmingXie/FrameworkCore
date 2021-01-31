using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrameworkCore.Api.ApiManager
{
    using AutoMapper;
    using FrameworkCore.EFCore;
    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact.Response;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class MenuManager : IMenuManager
    {
        private readonly ILogger<MenuManager> logger;
        private readonly IUnitOfWork work;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造函数-注入参数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="work"></param>
        /// <param name="mapper"></param>
        public MenuManager(ILogger<MenuManager> logger, IUnitOfWork work, IMapper mapper)
        {
            this.logger = logger;
            this.work = work;
            this.mapper = mapper;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Add(MenuDto param)
        {
            try
            {
                var menu = mapper.Map<Menu>(param);
                work.GetRepository<Menu>().Insert(menu);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(200, "");
                return new ApiResponse(201, "");
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "");
                return new ApiResponse(201, "");
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                var repository = work.GetRepository<Menu>();
                var user = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == id);
                if (user == null)
                    return new ApiResponse(201, "The Menu was not found");
                repository.Delete(user);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(200, "");
                return new ApiResponse(201, $"Delete post{ id } failed when saving !");
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "");
                return new ApiResponse(201, "");
            }
        }

        /// <summary>
        /// 获取所有菜单数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResponse> GetAll(QueryParameters param)
        {
            try
            {
                var models = await work.GetRepository<Menu>().GetPagedListAsync(
                    predicate: x =>
                     string.IsNullOrWhiteSpace(param.Search) ? true : x.MenuCode.Contains(param.Search) ||
                     string.IsNullOrWhiteSpace(param.Search) ? true : x.MenuName.Contains(param.Search),
                    pageIndex: param.PageIndex,
                    pageSize: param.PageSize);
                return new ApiResponse(200, models);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "");
                return new ApiResponse(201, "");
            }
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<ApiResponse> Save(MenuDto param)
        {
            try
            {
                var repository = work.GetRepository<Menu>();
                var dbmodel = await repository.GetFirstOrDefaultAsync(predicate: x => x.Id == param.Id);
                if (dbmodel == null) return new ApiResponse(201, "");
                dbmodel.MenuCode = param.MenuCode;
                dbmodel.MenuName = param.MenuName;
                dbmodel.MenuNameSpcace = param.MenuNameSpace;
                dbmodel.MenuAuth = param.MenuAuth;
                dbmodel.MenuCaption = param.MenuCaption;
                repository.Update(dbmodel);
                if (await work.SaveChangesAsync() > 0)
                    return new ApiResponse(200, "菜单保存成功");
                return new ApiResponse(201, "菜单保存失败");
            }
            catch (Exception ex)
            {
                logger.LogDebug(ex, "");
                return new ApiResponse(201, "");
            }
        }
    }
}
