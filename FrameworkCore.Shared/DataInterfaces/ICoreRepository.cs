/*****************************************************************************
*项目名称:FrameworkCore.Shared.DataInterfaces
*项目描述:
*类 名 称:ICoreRepository
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/28 22:59:29
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.Shared.DataInterfaces
{
    using FrameworkCore.Shared.Common.Collections;
    using FrameworkCore.Shared.Common.Query;
    using FrameworkCore.Shared.DataModel;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.HttpContact;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobDesignerRepository<T>
    {
        Task<BaseResponse<PagedList<T>>> GetAllListAsync(QueryParameters parameters);

        Task<BaseResponse<T>> GetAsync(int id);

        Task<BaseResponse> SaveAsync(T model);

        Task<BaseResponse> AddAsync(T model);

        Task<BaseResponse> DeleteAsync(int id);

        Task<BaseResponse> UpdateAsync(T model);
    }

    public interface IUserRepository : IJobDesignerRepository<UserDto>
    {
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<BaseResponse<UserInfoDto>> LoginAsync(string account, string password);

        /// <summary>
        /// 获取用户所属权限信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        Task<BaseResponse> GetUserPermByAccountAsync(string Account);

        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse<List<AuthItem>>> GetAuthListAsync();
    }

    public interface IGroupRepository : IJobDesignerRepository<GroupDto>
    {
        /// <summary>
        /// 获取菜单模块列表
        /// </summary>
        /// <returns></returns>
        Task<BaseResponse<List<MenuModuleGroupDto>>> GetMenuModuleListAsync();

        /// <summary>
        /// 根据用户Id获取组信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<BaseResponse<GroupDataDto>> GetGroupAsync(int id);

        /// <summary>
        /// 保存组数据
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        Task<BaseResponse> SaveGroupAsync(GroupDataDto group);
    }

    public interface IMenuRepository : IJobDesignerRepository<MenuDto>
    {

    }

    public interface IBasicRepository : IJobDesignerRepository<BasicDto>
    {

    }
}
