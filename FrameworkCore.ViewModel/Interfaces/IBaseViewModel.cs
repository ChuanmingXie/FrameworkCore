/*****************************************************************************
*项目名称:FrameworkCore.ViewModel.Interfaces
*项目描述:
*类 名 称:IBaseViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:11:31
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkCore.ViewModel.Interfaces
{
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.Shared.DataModel;
    using Microsoft.Toolkit.Mvvm.ComponentModel;
    using Microsoft.Toolkit.Mvvm.Input;
    using System.Threading.Tasks;

    #region 模块接口定义

    /// <summary>
    /// 定义基础的增删改查,分页,权限接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseViewModel<TEntity> : IOrdinary<TEntity>, IDataPager, IAuthority where TEntity : class { }

    /// <summary>
    /// 用户视图模型接口-定义用户数据的增删改查,分页,权限接口
    /// </summary>
    public interface IUserViewModel : IBaseViewModel<UserDto> { }

    /// <summary>
    /// 分组视图接口-定义用户数据的增删改查,分页,权限接口
    /// </summary>
    public interface IGroupViewModel : IBaseViewModel<GroupDto> { }

    /// <summary>
    /// 菜单数据接口-定义菜单数据的增删改查,分页,权限接口
    /// </summary>
    public interface IMenuViewModel : IBaseViewModel<MenuDto> { }

    /// <summary>
    /// 字典数据接口-定义字典数据的增删改查,分页,权限接口
    /// </summary>
    public interface IBasicViewModel : IBaseViewModel<BasicDto> { }

    #endregion

    #region 组件接口定义

    /// <summary>
    /// 定义基础组件
    /// </summary>
    public interface IComponentViewModel { }

    public interface ISkinVieModel : IComponentViewModel { }

    public interface IDashBoardViewModel : IComponentViewModel { }

    public interface IHomeViewModel : IComponentViewModel { }

    public interface ILoginViewModel : IComponentViewModel { }

    public interface IMainViewModel : IComponentViewModel
    {
        Task InitDefaultView();
    }

    #endregion

    #region 数据中心接口

    public interface ILoginCenter
    {
        Task<bool> ShowDialog();
    }

    public interface IMainCenter
    {
        Task<bool> ShowDialog();
    }

    public interface IMsgCenter
    {
        Task<bool> Show(object obj);
    }

    public interface IUserCenter : IBaseCenter { }

    public interface IMenuCenter : IBaseCenter { }

    public interface IGroupCenter : IBaseCenter { }

    public interface IBasicCenter : IBaseCenter { }

    public interface IHomeCenter : IBaseCenter { }

    public interface IDashboardCenter : IBaseCenter { }

    public interface ISkinCenter : IBaseCenter { }

    #endregion

    public interface IBaseDialog
    {
        bool DialogIsOpen { get; set; }

        void SnackBar(string msg);

        RelayCommand ExitCommand { get; }
    }
}
