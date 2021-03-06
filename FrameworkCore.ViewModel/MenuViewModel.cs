﻿/*****************************************************************************
*项目名称:FrameworkCore.ViewModel
*项目描述:
*类 名 称:MenuViewModel
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2021/1/29 1:24:44
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ Chuanmingxie 2021. All rights reserved
******************************************************************************/


namespace FrameworkCore.ViewModel
{
    using FrameworkCore.Shared.DataInterfaces;
    using FrameworkCore.Shared.Dto;
    using FrameworkCore.ViewModel.Interfaces;

    /// <summary>
    /// 菜单业务
    /// </summary>
    public class MenuViewModel : BaseRepository<MenuDto>, IMenuViewModel
    {
        public MenuViewModel(IMenuRepository repository) : base(repository)
        {

        }
    }
}
